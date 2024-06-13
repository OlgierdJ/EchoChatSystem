using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Interfaces.Bases;

namespace Echo.Domain.Shared.Interfaces.Services;

public interface IAccountService : IEntityService<Account, ulong>
{
    Task<Account> GetByUserId(ulong UserId);
}
