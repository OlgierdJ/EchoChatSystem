using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore.ChannelCore;
using CoreLib.DTO.RequestCore.ServerCore.Role;

namespace CoreLib.Interfaces.Services;

public interface IServerVoiceChannelService
{

    //invite
    /// <summary>
    /// verify user permission to invite (either owner or explicit permission),
    /// create invite,
    /// relay domainchanges to notificationhub
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    Task<bool> CreateVoiceChannelInvite(ulong userid, ulong serverid, ulong channelId, CreateInviteRequestDTO requestDTO);
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
    Task<bool> ConsumeVoiceChannelInvite(ulong userid, ulong serverid, string inviteCode);
    /// <summary>
    /// verify user permission to delete / edit invites, 
    /// softdelete invite, 
    /// relay domainchanges to notificationhub
    /// </summary>
    /// <param name="userid"></param>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    Task<bool> RemoveVoiceChannelInvite(ulong userid, ulong serverid, string inviteCode);

    //voicechannel
    Task<bool> CreateVoiceChannel(ulong userid, ulong serverid, CreateChannelRequestDTO requestDTO);
    Task<bool> MuteVoiceChannel(ulong userid, ulong serverid, ulong channelId, MuteRequestDTO requestDTO);
    Task<bool> UnmuteVoiceChannel(ulong userid, ulong serverid, ulong channelId);
    Task<bool> MoveVoiceChannel(ulong userid, ulong serverid, ulong channelId, MoveChannelRequestDTO requestDTO);
    Task<bool> DeleteVoiceChannel(ulong userid, ulong serverid, ulong channelId);
    Task<bool> UpdateVoiceChannel(ulong userid, ulong serverid, ulong channelId, UpdateVoiceChannelRequestDTO requestDTO);

    //role
    Task<bool> AddVoiceChannelRole(ulong userid, ulong serverid, ulong channelId, ulong roleId);
    Task<bool> SetVoiceChannelRolePermission(ulong userid, ulong serverid, ulong channelId, ulong roleId, SetPermissionStateRequestDTO requestDTO);
    Task<bool> SetVoiceChannelRolePermissions(ulong userid, ulong serverid, ulong channelId, ulong roleId, SetMultiplePermissionStateRequestDTO requestDTO);
    Task<bool> RemoveVoiceChannelRole(ulong userid, ulong serverid, ulong channelId, ulong roleId);

    //member
    Task<bool> AddVoiceChannelMember(ulong userid, ulong serverid, ulong channelId, ulong memberId);
    Task<bool> SetVoiceChannelMemberPermission(ulong userid, ulong serverid, ulong channelId, ulong memberId, SetPermissionStateRequestDTO requestDTO);
    Task<bool> SetVoiceChannelMemberPermissions(ulong userid, ulong serverid, ulong channelId, ulong memberId, SetMultiplePermissionStateRequestDTO requestDTO);
    Task<bool> RemoveVoiceChannelMember(ulong userid, ulong serverid, ulong channelId, ulong memberId);
}
