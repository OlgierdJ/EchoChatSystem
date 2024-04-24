using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        public UserRepository(EchoDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<User>> GetAllWithIncludeAsync(Expression<Func<User, bool>> expression = null)
        {
            return await QueryAll()
               .Include(e => e.Account)
               .Where(expression)
               .ToListAsync();
        }

        public override async Task<User> GetSingleWithIncludeAsync(Expression<Func<User, bool>> expression)
        {
            return await QueryAll()
                .Include(e => e.Account)
                .Where(expression)
                .FirstOrDefaultAsync();
        }
    }
}
