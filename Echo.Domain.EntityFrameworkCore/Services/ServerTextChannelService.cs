using AutoMapper;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using Echo.Application.Contracts.RequestCore.MessageCore;
using Microsoft.EntityFrameworkCore;
using Echo.Domain.Shared.Interfaces.Services;
using Echo.Domain.EntityFrameworkCore.EFCORE;
using Echo.Application.Contracts.DTO.RequestCore.ServerCore.ChannelCore;
using Echo.Application.Contracts.RequestCore.InviteCore;
using Echo.Application.Contracts.DTO.RequestCore.RelationCore;
using Echo.Application.Contracts.DTO.RequestCore.ServerCore.Role;

namespace Echo.Domain.EntityFrameworkCore.Services;

public class ServerTextChannelService : IServerTextChannelService
{
    private readonly EchoDbContext context;
    private readonly IMapper mapper;
    private readonly IPushNotificationService notificationService;

    public ServerTextChannelService(EchoDbContext context, IMapper mapper, IPushNotificationService notificationService)
    {
        this.context = context;
        this.mapper = mapper;
        this.notificationService = notificationService;
    }

    public async Task<bool> AddTextChannelMember(ulong senderId, ulong serverId, ulong channelId, ulong memberId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddTextChannelRole(ulong senderId, ulong serverId, ulong channelId, ulong roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ConsumeTextChannelInvite(ulong senderId, ulong serverId, string inviteCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateTextChannel(ulong senderId, ulong serverId, CreateChannelRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateTextChannelInvite(ulong senderId, ulong serverId, ulong channelId, CreateInviteRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteTextChannel(ulong senderId, ulong serverId, ulong channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> MarkTextChannelAsRead(ulong senderId, ulong serverId, ulong channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> MoveTextChannel(ulong senderId, ulong serverId, ulong channelId, MoveChannelRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> MuteTextChannel(ulong senderId, ulong serverId, ulong channelId, MuteRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PinTextChannelMessage(ulong senderId, ulong serverId, ulong channelId, ulong messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveTextChannelInvite(ulong senderId, ulong serverId, string inviteCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveTextChannelMember(ulong senderId, ulong serverId, ulong channelId, ulong memberId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveTextChannelMessage(ulong senderId, ulong serverId, ulong channelId, ulong messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveTextChannelRole(ulong senderId, ulong serverId, ulong channelId, ulong roleId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendTextChannelMessage(ulong senderId, ulong serverId, ulong channelId, SendMessageRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetTextChannelMemberPermission(ulong senderId, ulong serverId, ulong channelId, ulong memberId, SetPermissionStateRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetTextChannelMemberPermissions(ulong senderId, ulong serverId, ulong channelId, ulong memberId, SetMultiplePermissionStateRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetTextChannelRolePermission(ulong senderId, ulong serverId, ulong channelId, ulong roleId, SetPermissionStateRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SetTextChannelRolePermissions(ulong senderId, ulong serverId, ulong channelId, ulong roleId, SetMultiplePermissionStateRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnmuteTextChannel(ulong senderId, ulong serverId, ulong channelId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UnpinTextChannelMessage(ulong senderId, ulong serverId, ulong channelId, ulong messageId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateTextChannel(ulong senderId, ulong serverId, ulong channelId, UpdateTextChannelRequestDTO requestDTO)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> ValidateUserPermission(ServerProfile profileWithPermissions, ICollection<ServerPermission> permissions)
    {

        return true;
    }

    public async Task<bool> UpdateTextChannelMessage(ulong senderId, ulong serverId, ulong channelId, ulong messageId, EditMessageRequestDTO requestDTO)
    {
        try
        {
            //permission stuff is commented out for ease of use rn
            //verify sender is member
            var server = await context.Set<Server>()
                .Include(e => e.TextChannels.Where(e => e.Id == channelId).Take(1)).ThenInclude(e => e.Messages.Where(e => e.Id == messageId).Take(1))
                //.Include(e => e.TextChannels.Where(e => e.Id == channelId).Take(1)).ThenInclude(e=>e.Category).ThenInclude(e=>e.RolePermissions).ThenInclude(e=>e.Permission) //get textchannel role specific settings for this specific user
                //.Include(e => e.TextChannels.Where(e => e.Id == channelId).Take(1)).ThenInclude(e=>e.Category).ThenInclude(e=>e.MemberPermissions.Where(p=>p.AccountId == senderId)).ThenInclude(e=>e.Permission) //get textchannel role specific settings for this specific user
                //.Include(e => e.TextChannelMemberPermissions.Where(e=>e.ChannelId==channelId&&e.AccountId == senderId)).ThenInclude(e=>e.Permission) //get textchannel specific settings for this specific user
                //.Include(e => e.Members.Where(e=>e.AccountId == senderId).Take(1)).ThenInclude(e=>e.Roles).ThenInclude(e=>e.Role).ThenInclude(e=>e.Permissions) //get serverprofile and global permissions
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == serverId);
            //var senderAcc = await context.Set<Account>().Include(e => e.Roles).ThenInclude(e=>e.Permissions).AsSplitQuery().FirstOrDefaultAsync();

            //complex permission stuff here
            //var senderprofile = server.Members.FirstOrDefault();
            var textChannel = server.TextChannels.FirstOrDefault();
            if (/*senderprofile == null ||*/ textChannel == null)
            {
                return false;
            }

            //var PermissionIds = new List<ulong>(); //filled with granted permissions after calculating role hierarchy and permissionscope
            //var Permissions = new List<ServerPermission>(); //filled with permissions after filling permissionids

            //var expectedRole = "send messages";
            //var senderRoles=senderprofile.Roles.Select(e=>e.Role).OrderByDescending(e=>e.Importance); //users roles
            //var allRolePermissions=senderRoles.SelectMany(e=>e.Permissions); //users role grants across all roles
            //bool hasAdmin = false;
            //bool hasPermissionScopeGrant = false;
            ////whether or not the user has an administrator role and can skip rest of validation
            //var adminPermission = allRolePermissions.FirstOrDefault(e=>e.Permission.Name.ToLower() == "administrator" && e.State == true); 
            //if (adminPermission == null)
            //{
            //    hasAdmin = true;
            //    //break;
            //}

            //var allServerPermissions = allRolePermissions.Select(e=>e.Permission).DistinctBy(e=>e.Id);
            //var validatedPermission = allServerPermissions.FirstOrDefault(e=>e.Name.ToLower() == expectedRole); //instance is shared go from instance and check scopegrants from navigation
            //if (validatedPermission == null)
            //{
            //    return false;
            //}
            //if (textChannel.CategoryId!=null)
            //{
            //  hasPermissionScopeGrant=validatedPermission.CategoryMemberPermissions.Any(e => e.AccountId == senderId && e.ChannelCategoryId== textChannel.CategoryId && e.State!=null);
            //}
            //hasPermissionScopeGrant=validatedPermission.cate.Any(e => e.AccountId == senderId && e.ChannelId== channelId && e.State!=null);
            //hasPermissionScopeGrant=validatedPermission.RolePermissions.Any(e => e.AccountId == senderId && e.ChannelId== channelId && e.State!=null);
            //hasPermissionScopeGrant=validatedPermission.TextChannelMemberPermissions.FirstOrDefault(e => e.AccountId == senderId && e.ChannelId== channelId && e.State!=null).State;
            ////process scope grants
            //var globalGrant = validatedPermission.RolePermissions.Where(e => e.State == true).OrderByDescending(e=>e.PermissionId);

            ////null = unset and therefore ignore.
            ////if global active => active unless denied from further down
            ////if category active => overrides global unless denied further down
            ////if channel active => overrides category & global unless denied from memberpermission
            ////if memberpermission active => overrides channel, category & global.



            //var isMember = chat.Participants.Any(e => e.ParticipantId == senderId);
            //if (!hasAdmin  || hasPermissionScopeGrant)
            //{
            //    return false;
            //}

            var msg = textChannel.Messages.FirstOrDefault(e => e.Id == messageId);
            if (msg == null || msg.AuthorId != senderId)
            {
                return false;
            }
            //var request = await dbContext.Set<IncomingFriendRequest>().AsQueryable().Include(e => e.SenderRequest).FirstOrDefaultAsync(e => e.Id == requestId);
            msg.Content = requestDTO.Content;
            msg.TimeEdited = DateTime.Now;

            var res = await context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
}
