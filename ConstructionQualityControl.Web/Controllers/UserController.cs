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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            if (await unitOfWork.GetRepository<User>().GetAsync(u => u.Login == user.Login) != null) return Conflict();

            user.RegistrationDate = DateTime.Now;
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