using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Repositories;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories;

public class SecurityCredentialsRepository : BaseEntityRepository<SecurityCredentials>, ISecurityCredentialsRepository
{
    public SecurityCredentialsRepository(EchoDbContext context) : base(context)
    {
    }

    public override Task<IEnumerable<SecurityCredentials>> GetAllWithIncludeAsync(Expression<Func<SecurityCredentials, bool>> expression = null)
    {
        throw new NotImplementedException();
    }

    public override Task<SecurityCredentials> GetSingleWithIncludeAsync(Expression<Func<SecurityCredentials, bool>> expression)
    {
        throw new NotImplementedException();
    }
}
