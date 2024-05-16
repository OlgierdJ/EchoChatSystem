
using CoreLib.DTO.EchoCore.RequestCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
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

        public async Task<User> CreateUserAsync(RegisterRequestDTO input)
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

        public async Task<string> LoginUserAsync(string email, string password)
        {
            var user = await _repository.GetSingleWithIncludeAsync(e => e.Email == email);
            if (user is not null && await _pwdHandler.CheckPassword(password, user.Id))
            {
                //send a jwt back to the website
                TokenHandler tokenHandler = new();
                return tokenHandler.CreateToken<Account>(user.Account);
            }

            throw new Exception("You suck at hacking bruv");
        }
        public async Task<bool> UpdatePassword(ulong id, string password)
        {
            try
            {
                var result = await _pwdHandler.UpdatePassword(password, id); //get from jwt somewhere either in controller or in this service.
                return result is not null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
