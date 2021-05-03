using System;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationHandler handler;

        public AuthenticationController(IUnitOfWork unitOfWork, IMapper mapper, ICryptographer cryptographer)
        {
            handler = new AuthenticationHandler(unitOfWork, mapper, cryptographer);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, [FromBody] string password)
        {
            try
            {
                return Ok(await handler.LoginAsync(login, password));
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserData()
        {
            try
            {
                return Ok(await handler.GetCurrentUserDataAsync(HttpContext.User.Identity.Name));
            }
            catch (Exception) { return BadRequest(); }
        }
    }
}