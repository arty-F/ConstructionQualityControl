using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderHandler handler;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            handler = new OrderHandler(unitOfWork, mapper);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderDto)
        {
            try
            {
                await handler.CreateOrderAsync(orderDto);
                return Ok();
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(int id)
        {
            try
            {
                return Ok(await handler.GetOrderAsync(id, User.Claims));
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetOrders()
        {
            try
            {
                return Ok(await handler.GetOrdersAsync(User.Claims));
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet("Work/City/{id}")]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetWorksByCityId(int id)
        {
            try
            {
                return Ok(await handler.GetWorksByCityIdAsync(id));
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet("Work")]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetWorks()
        {
            try
            {
                return Ok(await handler.GetWorksAsync());
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet("Work/{id}")]
        public async Task<ActionResult<WorkReadDto>> GetWorkById(int id)
        {
            try
            {
                return Ok(await handler.GetWorkByIdAsync(id));
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpPost("Work/{id}")]
        public async Task<IActionResult> AddOffer(int id, WorkOfferCreateDto offerDto)
        {
            try
            {
                await handler.AddOfferAsync(id, offerDto);
                return Ok();
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ConfirmOffer(int id, WorkOfferReadDto offerDto)
        {
            try
            {
                await handler.ConfirmOfferAsync(id, offerDto, User.Claims);
                return Ok();
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception) { return BadRequest(); }
        }

        [HttpGet("Works")]
        public async Task<ActionResult<IEnumerable<OrderRootReadDto>>> GetConfirmedWorksForUser()
        {
            try
            {
                return Ok(await handler.GetConfirmedWorksForUserAsync(User.Claims));
            }
            catch (Exception) { return BadRequest(); }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<OrderReadDto>> ConfirmOrder(int id, OrderReadDto orderDto)
        {
            try
            {
                return Ok(await handler.ConfirmOrderAsync(id, orderDto, User.Claims));
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception) { return BadRequest(); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRootOrder(int id)
        {
            try
            {
                await handler.DeleteRootOrderAsync(id, User.Claims);
                return Ok();
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception) { return BadRequest(); }
        }
    }
}