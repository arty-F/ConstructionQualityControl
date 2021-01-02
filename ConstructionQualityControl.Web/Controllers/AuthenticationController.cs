using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Web.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ConstructionQualityControl.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, [FromBody]string password)
        {
            var checkUser = await unitOfWork.GetRepository<User>().GetAsync(u => u.Login == login);

            if (checkUser == null || checkUser.FirstOrDefault()?.Password != password) return Unauthorized();

            return Ok(JWTAuthenticationManager.GetToken(checkUser.First()));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var b = HttpContext.User;

            return Ok();
        }
    }
}