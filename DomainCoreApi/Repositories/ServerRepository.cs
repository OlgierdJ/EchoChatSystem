using CoreLib.Entities.EchoCore.ServerCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories
{
    public class ServerRepository : BaseEntityRepository<Server>, IServerRepository
    {
        public ServerRepository(EchoDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<Server>> GetAllWithIncludeAsync(Expression<Func<Server, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override Task<Server> GetSingleWithIncludeAsync(Expression<Func<Server, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
