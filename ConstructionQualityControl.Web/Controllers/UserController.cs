using System;
using System.Threading.Tasks;
using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionQualityControl.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICryptographer cryptographer;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, ICryptographer cryptographer)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cryptographer = cryptographer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            if (await unitOfWork.GetRepository<User>().GetAsync(u => u.Login == user.Login) != null) return Conflict();

            user.RegistrationDate = DateTime.Now;
            user.Password = cryptographer.Encypt(user.Password);
            user.City = await unitOfWork.GetRepository<City>().GetByIdAsync(userDto.City.Id);

            try
            {
                await unitOfWork.GetRepository<User>().AddAsync(user);
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}