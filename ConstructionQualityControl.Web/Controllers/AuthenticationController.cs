using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
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
        private readonly IMapper mapper;

        public AuthenticationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string login, [FromBody]string password)
        {
            var user = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(u => u.Login == login);

            if (user == null || user?.Password != password) return Unauthorized();

            return Ok(JWTAuthenticationManager.GetToken(mapper.Map<UserReadDto>(user)));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserData()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(u => u.Login == userName);

            return Ok(mapper.Map<UserReadDto>(user));
        }
    }
}