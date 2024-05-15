using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using CoreLib.Models;
using DomainCoreApi.Handlers;
using DomainCoreApi.Services.Bases;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DomainCoreApi.Services
{
    public class UserService : BaseEntityService<User, ulong>, IUserService
    {

        private readonly IPasswordHandler _pwdHandler;
        private readonly IAccountService _accountService;
        private readonly CreateUserHandler _createUserHandler = new();
        public UserService(IUserRepository repository, IPasswordHandler pwdHandler, IAccountService accountService) : base(repository)
        {
            _pwdHandler = pwdHandler;
            _accountService = accountService;
        }

        public async Task<User> CreateUserAsync(RegisterUserModel input)
        {
            try
            {
                Console.WriteLine(input);
                var data = await _createUserHandler.CreateHandler(input);
                var result = await _repository.AddAsync(data.Item1);
                data.Item2.UserId = result.Id;
                await _pwdHandler.CreatePassword(input.Password, data.Item1.Id);
                await _accountService.AddAsync(data.Item2);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }   
        }

        public async Task<string> LoginUserAsync(UserLoginModel attempt)
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
        public async Task<bool> UpdatePassword(UpdatePasswordModel update)
        {
            try
            {
                var result = await _pwdHandler.UpdatePassword(update.Password, update.Id);
                return result is not null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
