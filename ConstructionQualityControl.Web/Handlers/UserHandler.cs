using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Web.Handlers
{
    public class UserHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICryptographer cryptographer;

        public UserHandler(IUnitOfWork unitOfWork, IMapper mapper, ICryptographer cryptographer)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cryptographer = cryptographer;
        }

        public async Task CreateUserAsync(UserCreateDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            if (await unitOfWork.GetRepository<User>().GetAsync(u => u.Login == user.Login) != null)
                throw new ArgumentException();

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
                throw;
            }
        }
    }
}
