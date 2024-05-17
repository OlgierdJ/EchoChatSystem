using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Bases;

namespace CoreLib.Interfaces.Services
{
    public interface IAccountService : IEntityService<Account, ulong>
    {
        Task<Account> GetByUserId(ulong UserId);
    }
}
