using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderDto)
        {
            var city = await unitOfWork.GetRepository<City>().GetByIdAsync(orderDto.City.Id);
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(orderDto.User.Id);
            var order = MapperHelper.MapRecursiveToNewOrder(orderDto, user, city);

            try
            {
                await unitOfWork.GetRepository<Order>().AddAsync(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(int id)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null)
                return BadRequest();

            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (order.User.Id != userId && order.WorkOffers.FirstOrDefault()?.Worker.Id != userId)
                return Unauthorized();

            return Ok(mapper.Map<OrderReadDto>(order));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetOrders()
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            IEnumerable<Order> orders = role.Value switch
            {
                "Customer" => await GetCustomerOrders(userId),
                "Builder" => await GetAllUnstartedOrders(),
                _ => throw new Exception("Unsupported user role.")
            };

            var ordersDto = mapper.Map<List<OrderRootReadDto>>(orders);
            return Ok(ordersDto);
        }

        private async Task<IEnumerable<Order>> GetCustomerOrders(int customerId)
        {
            return await unitOfWork.GetRepository<Order>().GetAsync(o => o.User.Id == customerId && o.IsRoot == true);
        }

        private async Task<IEnumerable<Order>> GetAllUnstartedOrders()
        {
            return await unitOfWork.GetRepository<Order>().GetAsync(o => o.IsRoot && !o.IsStarted);
        }

        [HttpGet("Work/City/{id}")]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetWorksByCityId(int id)
        {
            var orders = await unitOfWork.GetRepository<Order>().GetAsync(o => o.IsRoot && !o.IsStarted && o.City.Id == id);
            return Ok(mapper.Map<List<OrderRootReadDto>>(orders));
        }

        [HttpGet("Work")]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetWorks()
        {
            var orders = await unitOfWork.GetRepository<Order>().GetAsync(o => o.IsRoot && !o.IsStarted);
            return Ok(mapper.Map<List<OrderRootReadDto>>(orders));
        }

        [HttpGet("Work/{id}")]
        public async Task<ActionResult<WorkReadDto>> GetWorkById(int id)
        {
            var work = await unitOfWork.GetRepository<Order>().GetFirstOrDefaultAsync(o => o.Id == id);
            return Ok(mapper.Map<WorkReadDto>(work));
        }

        [HttpPost("Work/{id}")]
        public async Task<IActionResult> AddOffer(int id, WorkOfferCreateDto offerDto)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            if (order == null || order.IsStarted)
                return BadRequest();

            var offer = mapper.Map<WorkOffer>(offerDto);
            offer.Worker = await unitOfWork.GetRepository<User>().GetByIdAsync(offer.Worker.Id);
            offer.Date = DateTime.Now;
            order.WorkOffers.Add(offer);

            try
            {
                unitOfWork.GetRepository<Order>().Update(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ConfirmOffer(int id, WorkOfferReadDto offerDto)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);

            if (order.User.Id != int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value))
                return BadRequest();

            order.WorkOffers.RemoveAll(o => o.Id != offerDto.Id);
            order.IsStarted = true;
            if (order.SubOrders.Count > 0)
                order.SubOrders.FirstOrDefault().IsStarted = true;

            try
            {
                unitOfWork.GetRepository<Order>().Update(order);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("Works")]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetConfirmedWorksForUser()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var works = await unitOfWork.GetRepository<Order>().GetAsync(ord => ord.WorkOffers.FirstOrDefault().Worker.Id == userId && ord.IsStarted);
            return Ok(mapper.Map<List<OrderRootReadDto>>(works));
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<OrderReadDto>> ConfirmOrder(int id, OrderReadDto orderDto)
        {
            var rootOrder = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(orderDto.Id);
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (order.User.Id != userId)
                return BadRequest();
            order.IsCompleted = true;

            Order nextOrder = null;
            foreach (var o in rootOrder.SubOrders)
            {
                if (nextOrder != null)
                    break;

                if (!o.IsStarted)
                {
                    nextOrder = o;
                    break;
                }

                foreach (var so in o.SubOrders)
                {
                    if (!so.IsStarted)
                    {
                        nextOrder = so;
                        break;
                    }
                }
            }

            if (nextOrder != null)
                nextOrder.IsStarted = true;
            else
                rootOrder.IsCompleted = true;

            try
            {
                unitOfWork.GetRepository<Order>().Update(rootOrder);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(mapper.Map<OrderReadDto>(rootOrder));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRootOrder(int id)
        {
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(id);
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            if (order.User.Id != userId || order.IsStarted)
                return BadRequest();

            try
            {
                var repo = unitOfWork.GetRepository<Order>();

                foreach (var o in order.SubOrders)
                {
                    foreach (var so in o.SubOrders)
                        await repo.DeleteByIdAsync(so.Id);

                    await repo.DeleteByIdAsync(o.Id);
                }
                await unitOfWork.GetRepository<Order>().DeleteByIdAsync(id);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
