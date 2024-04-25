using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Handlers;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using CoreLib.Models;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class UserService : BaseEntityService<User, ulong>, IUserService
    {

        private readonly IPasswordHandler _pwdHandler;
        private readonly IAccountService _accountService;
        public UserService(IUserRepository repository, IPasswordHandler pwdHandler,IAccountService accountService) : base(repository)
        {
            _pwdHandler = pwdHandler;
            _accountService = accountService;
        }

        public Task<User> CreateUserAsync(User input, string pword)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginUserAsync(UserLogins attempt)
        {
            var user = await _repository.GetSingleWithIncludeAsync(e => e.Email == attempt.Email);
            if (user is not null && await _pwdHandler.CheckPassword(attempt.Password, user.Id))
            {
                //send a jwt back to the website
                TokenHandler tokenHandler = new();
                return tokenHandler.CreateToken<Account>(user.Account);
            }

            throw new Exception("You suck at hacking bruv");
        }
    }
}
