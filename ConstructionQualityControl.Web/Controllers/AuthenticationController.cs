﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConstructionQualityControl.Controllers.Web;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Web.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ConstructionQualityControl.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<WeatherForecastController> logger;

        public AuthenticationController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [Route("{login}")]
        [HttpPost]
        public async Task<IActionResult> Create(string login, [FromBody]string password)
        {
            var checkUser = await unitOfWork.GetRepository<User>().GetAsync(u => u.Login == login);

            if (checkUser == null)
            {
                return BadRequest();
            }

            var user = checkUser.First();

            if (user.Password != password)
            {
                return BadRequest();
            }

            return new ObjectResult(JWTAuthenticationManager.GetToken(user));
        }

        [Authorize]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}