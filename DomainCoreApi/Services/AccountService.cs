using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class AccountService : BaseEntityService<Account, ulong> , IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository repository) : base(repository)
        {
            _accountRepository = repository;
        }

        public async Task<Account> GetByUserId(ulong UserId)
        {
            return await _accountRepository.GetSingleAsync(e => e.UserId == UserId);
        }
    }
}
