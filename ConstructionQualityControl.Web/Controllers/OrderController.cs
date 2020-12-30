using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
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
            //var order = mapper.Map<Order>(orderDto);
            //var order = MapperHelper.MapRecursiveToNewOrder(orderDto);

            /*foreach (var subOrd in order.SubOrders)
            {
                subOrd.CreationDate = DateTime.Now;
                subOrd.IsActive = true;
                foreach (var o in subOrd.SubOrders)
                {
                    o.CreationDate = DateTime.Now;
                    o.IsActive = true;
                }
            }*/

            return Ok();
        }
    }
}
