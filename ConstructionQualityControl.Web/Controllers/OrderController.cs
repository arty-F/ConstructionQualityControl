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
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetOrders()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var orders = await unitOfWork.GetRepository<Order>().GetAsync(o => o.User.Id == userId && o.IsRoot == true);
            var a = mapper.Map<List<OrderRootReadDto>>(orders);
            return Ok(a);
        }
    }
}
