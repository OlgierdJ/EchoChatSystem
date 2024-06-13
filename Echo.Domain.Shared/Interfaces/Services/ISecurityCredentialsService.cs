using Echo.Domain.Shared.Entities.EchoCore.UserCore;
using Echo.Domain.Shared.Interfaces.Bases;

namespace Echo.Domain.Shared.Interfaces.Services;

public interface ISecurityCredentialsService : IEntityService<SecurityCredentials, ulong>
{
}
