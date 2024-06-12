using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Interfaces.Services;
using DomainCoreApi.EFCORE;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.Services;

public class UserGroupService : IUserGroupService
{
    private readonly EchoDbContext dbContext;

    public UserGroupService(EchoDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<IEnumerable<string>> GetGroups(ulong accountId)
    {
        try
        {
            var acc = await dbContext.Set<Account>()
                .Include(e => e.Chats)
                .Include(e => e.Servers)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == accountId);
            var groups = new List<string>();
            groups.AddRange(acc.Chats.Select(chat => $"{nameof(Chat)}/{chat.SubjectId}"));
            groups.AddRange(acc.Servers.Select(chat => $"{nameof(Server)}/{chat.ServerId}"));
            return groups;
        }
        catch (Exception e)
        {
            //log here then throw
            throw;
        }
    }
}
