using System.Linq;
using System.Threading.Tasks;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Web.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthenticationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("{login}")]
        [HttpPost]
        public async Task<IActionResult> Create(string login, [FromBody]string password)
        {
            var checkUser = await unitOfWork.GetRepository<User>().GetAsync(u => u.Login == login);

            if (checkUser == null || checkUser.FirstOrDefault()?.Password != password) return BadRequest();

            return new ObjectResult(JWTAuthenticationManager.GetToken(checkUser.First()));
        }
    }
}