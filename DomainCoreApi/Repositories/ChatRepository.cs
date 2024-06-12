using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Interfaces.Repositories;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories;

public class ChatRepository : BaseEntityRepository<Chat>, IChatRepository
{
    public ChatRepository(EchoDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Chat>> GetAllWithIncludeAsync(Expression<Func<Chat, bool>> expression = null)
    {
        return await QueryAll()
           .Include(e => e.MessageTrackers)
           .Where(expression)
           .ToListAsync();
    }

    public override async Task<Chat> GetSingleWithIncludeAsync(Expression<Func<Chat, bool>> expression)
    {
        return await QueryAll()
          .Include(e => e.MessageTrackers)
          .Where(expression)
          .FirstOrDefaultAsync();
    }
}
//chatService