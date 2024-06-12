using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Repositories;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services;

public class SecurityCredentialsService : BaseEntityService<SecurityCredentials, ulong>, ISecurityCredentialsService
{
    public SecurityCredentialsService(ISecurityCredentialsRepository repository) : base(repository)
    {
    }

}
