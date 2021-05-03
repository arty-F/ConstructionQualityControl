using System;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Web.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserHandler handler;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, ICryptographer cryptographer)
        {
            handler = new UserHandler(unitOfWork, mapper, cryptographer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            try
            {
                await handler.CreateUserAsync(userDto);
                return Ok();
            }
            catch (ArgumentException) { return Conflict(); }
            catch (Exception) { return BadRequest(); }
        }
    }
}