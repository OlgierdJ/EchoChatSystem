using CoreLib.DTO.EchoCore.RequestCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Handlers;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class UserService : BaseEntityService<User, ulong>, IUserService
    {

        private readonly IPasswordHandler _pwdHandler;
        private readonly IAccountService _accountService;
        private readonly ILanguageRepository _languageRepository;
        private readonly CreateUserHandler _createUserHandler = new();
        public UserService(IUserRepository repository, IPasswordHandler pwdHandler, IAccountService accountService, ILanguageRepository languageRepository) : base(repository)
        {
            _pwdHandler = pwdHandler;
            _accountService = accountService;
            _languageRepository = languageRepository;
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
                data.Item2.Settings.Language = await _languageRepository.GetSingleAsync(b => b.Id == 10);
                await _accountService.AddAsync(data.Item2);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<string> LoginUserAsync(LoginRequestDTO attempt)
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
        public async Task<bool> UpdatePassword(ulong id, string password)
        {
            try
            {
                var result = await _pwdHandler.UpdatePassword(password, id);
                return result is not null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
