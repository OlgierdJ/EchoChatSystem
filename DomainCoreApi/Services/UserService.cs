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

        public async Task<User> LoginUserAsync(UserLogins attempt)
        {
            //var user = await _repository.GetSingleWithIncludeAsync(e => e.Email == attempt.Email);
            //if (user is not null && await _pwdHandler.CheckPassword(attempt.Password,user.Id))
            //{
            //    //send a jwt back to the website
            //    return user;
            //}
            User user = new() { Email = "admin@tec.dk",Id=1,DateOfBirth=DateTime.UtcNow,PasswordSetDate=DateTime.UtcNow };
            if (attempt.Email == user.Email )
            {
                return user;
            }

            throw new Exception("You suck at hacking bruv");
        }
    }
}
