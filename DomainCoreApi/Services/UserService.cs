using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class UserService : BaseEntityService<User, ulong>, IUserService
    {
        private readonly IPasswordHandler _pwdHandler;
        public UserService(IUserRepository repository, IPasswordHandler pwdHandler) : base(repository)
        {
            _pwdHandler = pwdHandler;
        }

        public Task<User> CreateUserAsync(User input, string pword)
        {
            throw new NotImplementedException();
        }

        public Task<User> LoginUserAsync(UserLogins attempt)
        {
            throw new NotImplementedException();
        }
    }
}
