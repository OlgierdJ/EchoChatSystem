﻿using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;
using Microsoft.EntityFrameworkCore;
using Echo.Domain.Shared.Entities.EchoCore.ChatCore;
using Echo.Domain.Shared.Interfaces.Services;
using Echo.Domain.EntityFrameworkCore.EFCORE;

namespace Echo.Domain.EntityFrameworkCore.Services;

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