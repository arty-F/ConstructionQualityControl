using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
        {
            var users = await unitOfWork.GetRepository<User>().GetAsync();
            if (users == null) return NotFound();
            return Ok(mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserById(int id)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(mapper.Map<UserReadDto>(user));
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

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserReadDto userDto)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (userDto.Id != userId || userDto.Role != userRole) return BadRequest();

            try
            {
                unitOfWork.GetRepository<User>().Update(mapper.Map<User>(userDto));
                await unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value != RolesManager.Admin) return BadRequest();

            try
            {
                await unitOfWork.GetRepository<User>().DeleteByIdAsync(id);
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