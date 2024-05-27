using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.MessageCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore.ChannelCore;
using CoreLib.DTO.RequestCore.ServerCore.Role;

namespace CoreLib.Interfaces.Services
{
    public interface IServerTextChannelService
    {
        //textchannel
        Task<bool> CreateTextChannel(ulong userid, ulong serverid, CreateChannelRequestDTO requestDTO);
        Task<bool> MuteTextChannel(ulong userid, ulong serverid, ulong channelId, MuteRequestDTO requestDTO);
        Task<bool> UnmuteTextChannel(ulong userid, ulong serverid, ulong channelId);
        Task<bool> MoveTextChannel(ulong userid, ulong serverid, ulong channelId, MoveChannelRequestDTO requestDTO);
        Task<bool> DeleteTextChannel(ulong userid, ulong serverid, ulong channelId);
        Task<bool> UpdateTextChannel(ulong userid, ulong serverid, ulong channelId, UpdateTextChannelRequestDTO requestDTO);
        Task<bool> MarkTextChannelAsRead(ulong userid, ulong serverid, ulong channelId);
        //message
        Task<bool> SendTextChannelMessage(ulong userid, ulong serverid, ulong channelId, SendMessageRequestDTO requestDTO);
        Task<bool> RemoveTextChannelMessage(ulong userid, ulong serverid, ulong channelId, ulong messageId);
        Task<bool> PinTextChannelMessage(ulong userid, ulong serverid, ulong channelId, ulong messageId);
        Task<bool> UpdateTextChannelMessage(ulong userid, ulong serverid, ulong channelId, ulong messageId, EditMessageRequestDTO requestDTO);
        Task<bool> UnpinTextChannelMessage(ulong userid, ulong serverid, ulong channelId, ulong messageId);

        //role
        Task<bool> AddTextChannelRole(ulong userid, ulong serverid, ulong channelId, ulong roleId);
        Task<bool> SetTextChannelRolePermission(ulong userid, ulong serverid, ulong channelId, ulong roleId, SetPermissionStateRequestDTO requestDTO);
        Task<bool> SetTextChannelRolePermissions(ulong userid, ulong serverid, ulong channelId, ulong roleId, SetMultiplePermissionStateRequestDTO requestDTO);
        Task<bool> RemoveTextChannelRole(ulong userid, ulong serverid, ulong channelId, ulong roleId);

        //member
        Task<bool> AddTextChannelMember(ulong userid, ulong serverid, ulong channelId, ulong memberId);
        Task<bool> SetTextChannelMemberPermission(ulong userid, ulong serverid, ulong channelId, ulong memberId, SetPermissionStateRequestDTO requestDTO);
        Task<bool> SetTextChannelMemberPermissions(ulong userid, ulong serverid, ulong channelId, ulong memberId, SetMultiplePermissionStateRequestDTO requestDTO);
        Task<bool> RemoveTextChannelMember(ulong userid, ulong serverid, ulong channelId, ulong memberId);

        //invite
        /// <summary>
        /// verify user permission to invite (either owner or explicit permission),
        /// create invite,
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> CreateTextChannelInvite(ulong userid, ulong serverid, ulong channelId, CreateInviteRequestDTO requestDTO);
        /// <summary>
        /// verify invite still exists and user is allowed to join (if they are not a part of the thing already and are not banned),
        /// consume invite updating its usecount, 
        /// create serverprofile linking join method,
        /// default roles (everyone), 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> ConsumeTextChannelInvite(ulong userid, ulong serverid, string inviteCode);
        /// <summary>
        /// verify user permission to delete / edit invites, 
        /// softdelete invite, 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> RemoveTextChannelInvite(ulong userid, ulong serverid, string inviteCode);
    }

}
