using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.ServerCore.EmoteCore;
using CoreLib.DTO.RequestCore.ServerCore.Event;
using CoreLib.DTO.RequestCore.ServerCore.Role;
using CoreLib.DTO.RequestCore.ServerCore.Server;
using CoreLib.DTO.RequestCore.ServerCore.SoundboardSound;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.Enums;
using CoreLib.Interfaces.Bases;
using System.Threading.Channels;

namespace CoreLib.Interfaces.Services
{

    public interface IServerService
    {
        //server
        Task<bool> CreateServer(ulong userid, CreateServerRequestDTO requestDTO); //create new server with name and public flag and ownerid as serverowner, relay domainchanges to notificationhub.
        Task<bool> DeleteServer(ulong userid, ulong serverId); //verify user by userid is owner, delete server and related entities, relay domainchanges to notificationhub.
        Task<bool> EditServer(ulong userid, ulong serverid, EditServerRequestDTO requestDTO); //verify user has permission to update server (either owner or has explicit permission), update server, relay domainchanges to notificationhub.
        Task<bool> MarkAsRead(ulong userid, ulong serverId); //marks everything in server as read.
        Task<bool> MuteServer(ulong userid, MuteRequestDTO requestDTO); //mutes server (doesnt join signalr group)
        Task<bool> UnmuteServer(ulong userid, ulong serverId); //unmutes server (allows join signalr group)
        Task<bool> SetServerImage(ulong userid, ulong serverid, SetImageRequestDTO requestDTO); //uploads and uses only serverimage
        Task<bool> LeaveServer(ulong userid, ulong serverId);
        
        //invite
        /// <summary>
        /// verify user permission to invite (either owner or explicit permission),
        /// create invite,
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> CreateInvite(ulong userid, ulong serverid, InviteType type, CreateInviteRequestDTO requestDTO);
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
        Task<bool> ConsumeInvite(ulong userid, ulong serverid, InviteType type, string inviteCode);
        /// <summary>
        /// verify user permission to delete / edit invites, 
        /// softdelete invite, 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> RemoveInvite(ulong userid, ulong serverid, InviteType type, string inviteCode);

        //event
        /// <summary>
        /// verify userpermissions to create event,
        /// create event,
        /// auto join user as interested, 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> CreateEvent(ulong userid, ulong serverid, CreateEventRequestDTO requestDTO);
        Task<bool> StartEvent(ulong userid, ulong serverid, ulong eventId);
        Task<bool> EditEvent(ulong userid, ulong serverid, EditEventRequestDTO requestDTO);
        Task<bool> JoinEvent(ulong userid, ulong serverid, ulong eventId);
        Task<bool> LeaveEvent(ulong userid, ulong serverid, ulong eventId);
        Task<bool> DeleteEvent(ulong userid, ulong serverid, ulong eventId);

        //emote
        Task<bool> CreateEmote(ulong userid, ulong serverid, CreateEmoteRequestDTO requestDTO);
        Task<bool> EditEmote(ulong userid, ulong serverid, EditEmoteRequestDTO requestDTO);
        Task<bool> DeleteEmote(ulong userid, ulong serverid, ulong emoteId);

        //soundboard
        Task<bool> CreateSoundboardSound(ulong userid, ulong serverid, CreateSoundboardSoundRequestDTO requestDTO);
        Task<bool> EditSoundboardSound(ulong userid, ulong serverid, EditSoundboardSoundRequestDTO requestDTO);
        Task<bool> DeleteSoundboardSound(ulong userid, ulong serverid, ulong soundId);

        //role
        Task<bool> CreateRole(ulong userid, ulong serverid, CreateRoleRequestDTO requestDTO);
        Task<bool> EditRole(ulong userid, ulong serverid, EditRoleRequestDTO requestDTO);
        Task<bool> SetRolePermission(ulong userid, ulong serverid, SetPermissionStateRequestDTO requestDTO);
        Task<bool> SetRolePermissions(ulong userid, ulong serverid, SetMultiplePermissionStateRequestDTO requestDTO);
        Task<bool> DeleteRole(ulong userid, ulong serverid, ulong roleId);
    }

}
