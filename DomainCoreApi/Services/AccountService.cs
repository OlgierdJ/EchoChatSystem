using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class AccountService : BaseEntityService<Account, ulong> , IAccountService
    {
        public AccountService(IAccountRepository repository) : base(repository)
        {
        }

        public async Task<Account> GetByUserId(ulong UserId)
        {
            return await _repository.GetSingleAsync(e => e.UserId == UserId);
        }
    }
}
