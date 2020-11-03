using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ConstructionQualityControl.Controllers.Web;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString()),
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("CHANGETHIS!CHANGETHIS!CHANGETHIS!")), SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(credentials);
            JwtPayload payload = new JwtPayload(claims);
            var token = new JwtSecurityToken(header, payload);

            var result = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = login
            };

            return new ObjectResult(result);
        }

        [Authorize]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}