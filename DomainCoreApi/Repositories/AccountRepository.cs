using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Repositories;
using CoreLib.Repositories.Bases;
using DomainCoreApi.EFCORE;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DomainCoreApi.Repositories;

public class AccountRepository : BaseEntityRepository<Account>, IAccountRepository
{
    public AccountRepository(EchoDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Account>> GetAllWithIncludeAsync(Expression<Func<Account, bool>> expression = null)
    {
        return await QueryAll()
          .Include(e => e.ActivityStatus)
          .Include(e => e.CustomStatus)
          .Include(e => e.Profile)
          .Include(e => e.Connections)
          .Include(e => e.Settings)
          .Include(e => e.Roles)
          .Include(e => e.Violations)
          .Include(e => e.IssuedViolations)
          .Include(e => e.ReviewedAppeals)
          .Include(e => e.Sessions)
          .Include(e => e.BlockedAccounts)
          .Include(e => e.NicknamedAccounts)
          .Include(e => e.NotedAccounts)
          .Include(e => e.MutedVoices)
          .Include(e => e.MutedChats)
          .Include(e => e.MutedSoundboards)
          .Include(e => e.ChatMessageTrackers)
          .Include(e => e.TextChannelMessageTrackers)
          .Include(e => e.ReportedCustomStatuses)
          .Include(e => e.CustomStatusReports)
          .Include(e => e.ReportedProfiles)
          .Include(e => e.ProfileReports)
          .Include(e => e.ReportedMessages)
          .Include(e => e.MessageReports)
          .Include(e => e.IncomingFriendRequests)
          .Include(e => e.OutgoingFriendRequests)
          .Include(e => e.Friendships)
          .Include(e => e.FriendSuggestions)
          .Include(e => e.Chats)
          .Include(e => e.ChatInvites)
          .Include(e => e.ChatMessages)
          .Include(e => e.Servers)
          .Include(e => e.ServerInvites)
          .Include(e => e.ChannelMessages).AsSplitQuery()
          .Where(expression)
          .ToListAsync();
    }

    public override async Task<Account> GetSingleWithIncludeAsync(Expression<Func<Account, bool>> expression)
    {
        return await QueryAll()
          .Include(e => e.ActivityStatus)
          .Include(e => e.CustomStatus)
          .Include(e => e.Profile)
          .Include(e => e.Connections)
          .Include(e => e.Settings)
          .Include(e => e.Roles)
          .Include(e => e.Violations)
          .Include(e => e.IssuedViolations)
          .Include(e => e.ReviewedAppeals)
          .Include(e => e.Sessions)
          .Include(e => e.BlockedAccounts)
          .Include(e => e.NicknamedAccounts)
          .Include(e => e.NotedAccounts)
          .Include(e => e.MutedVoices)
          .Include(e => e.MutedChats)
          .Include(e => e.MutedSoundboards)
          .Include(e => e.ChatMessageTrackers)
          .Include(e => e.TextChannelMessageTrackers)
          .Include(e => e.ReportedCustomStatuses)
          .Include(e => e.CustomStatusReports)
          .Include(e => e.ReportedProfiles)
          .Include(e => e.ProfileReports)
          .Include(e => e.ReportedMessages)
          .Include(e => e.MessageReports)
          .Include(e => e.IncomingFriendRequests)
          .Include(e => e.OutgoingFriendRequests)
          .Include(e => e.Friendships)
          .Include(e => e.FriendSuggestions)
          .Include(e => e.Chats)
          .Include(e => e.ChatInvites)
          .Include(e => e.ChatMessages)
          .Include(e => e.Servers)
          .Include(e => e.ServerInvites)
          .Include(e => e.ChannelMessages).AsSplitQuery()
          .Where(expression)
          .FirstOrDefaultAsync();
    }
}
