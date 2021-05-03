using AutoMapper;
using ConstructionQualityControl.Data.Models;
using ConstructionQualityControl.Domain;
using ConstructionQualityControl.Domain.Dtos;
using ConstructionQualityControl.Web.Authentication;
using System;
using System.Threading.Tasks;

namespace ConstructionQualityControl.Web.Handlers
{
    public class AuthenticationHandler
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICryptographer cryptographer;

        public AuthenticationHandler(IUnitOfWork unitOfWork, IMapper mapper, ICryptographer cryptographer)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cryptographer = cryptographer;
        }

        public async Task<object> LoginAsync(string login, string password)
        {
            var user = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(u => u.Login == login);

            if (user == null || cryptographer.Decrypt(user.Password) != password) throw new UnauthorizedAccessException();

            return JWTAuthenticationManager.GetToken(mapper.Map<UserReadDto>(user));
        }

        public async Task<UserReadDto> GetCurrentUserDataAsync(string contextedUserName)
        {
            var user = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(u => u.Login == contextedUserName);
            return mapper.Map<UserReadDto>(user);
        }
    }
}
