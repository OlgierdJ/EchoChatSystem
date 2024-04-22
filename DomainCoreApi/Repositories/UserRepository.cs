using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        public UserRepository(EchoDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<User>> GetAllWithIncludeAsync(Expression<Func<User, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public override Task<User> GetSingleWithIncludeAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
