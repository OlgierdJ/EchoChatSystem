using Echo.Domain.Shared.Entities.EchoCore.UserCore;
using Echo.Domain.EntityFrameworkCore.Services.Bases;
using Echo.Domain.Shared.Interfaces.Repositories;
using Echo.Domain.Shared.Interfaces.Services;

namespace Echo.Domain.EntityFrameworkCore.Services;

public class SecurityCredentialsService : BaseEntityService<SecurityCredentials, ulong>, ISecurityCredentialsService
{
    public SecurityCredentialsService(ISecurityCredentialsRepository repository) : base(repository)
    {
    }

}
