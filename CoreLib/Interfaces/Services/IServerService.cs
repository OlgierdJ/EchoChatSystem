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
        Task<bool> CreateServer(ulong senderId, CreateServerRequestDTO requestDTO); //create new server with name and public flag and ownerid as serverowner, relay domainchanges to notificationhub.
        Task<bool> DeleteServer(ulong senderId, ulong serverId); //verify user by senderId is owner, delete server and related entities, relay domainchanges to notificationhub.
        Task<bool> EditServer(ulong senderId, ulong serverid, EditServerRequestDTO requestDTO); //verify user has permission to update server (either owner or has explicit permission), update server, relay domainchanges to notificationhub.
        Task<bool> MarkAsRead(ulong senderId, ulong serverId); //marks everything in server as read.
        Task<bool> MuteServer(ulong senderId, ulong serverId, MuteRequestDTO requestDTO); //mutes server (doesnt join signalr group)
        Task<bool> UnmuteServer(ulong senderId, ulong serverId); //unmutes server (allows join signalr group)
        Task<bool> SetServerImage(ulong senderId, ulong serverid, SetImageRequestDTO requestDTO); //uploads and uses only serverimage
        Task<bool> LeaveServer(ulong senderId, ulong serverId);
        
        //invite
        /// <summary>
        /// verify user permission to invite (either owner or explicit permission),
        /// create invite,
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> CreateInvite(ulong senderId, ulong serverid, CreateInviteRequestDTO requestDTO);
        /// <summary>
        /// verify invite still exists and user is allowed to join (if they are not a part of the thing already and are not banned),
        /// consume invite updating its usecount, 
        /// create serverprofile linking join method,
        /// default roles (everyone), 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> ConsumeInvite(ulong senderId, ulong serverid, string inviteCode);
        /// <summary>
        /// verify user permission to delete / edit invites, 
        /// softdelete invite, 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> RemoveInvite(ulong senderId, ulong serverid, string inviteCode);

        //event
        /// <summary>
        /// verify userpermissions to create event,
        /// create event,
        /// auto join user as interested, 
        /// relay domainchanges to notificationhub
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
        Task<bool> CreateEvent(ulong senderId, ulong serverid, CreateEventRequestDTO requestDTO);
        Task<bool> StartEvent(ulong senderId, ulong serverid, ulong eventId);
        Task<bool> EditEvent(ulong senderId, ulong serverid, ulong eventId, EditEventRequestDTO requestDTO);
        Task<bool> JoinEvent(ulong senderId, ulong serverid, ulong eventId);
        Task<bool> LeaveEvent(ulong senderId, ulong serverid, ulong eventId);
        Task<bool> DeleteEvent(ulong senderId, ulong serverid, ulong eventId);

        //emote
        Task<bool> CreateEmote(ulong senderId, ulong serverid, CreateEmoteRequestDTO requestDTO);
        Task<bool> EditEmote(ulong senderId, ulong serverid, ulong emoteId, EditEmoteRequestDTO requestDTO);
        Task<bool> DeleteEmote(ulong senderId, ulong serverid, ulong emoteId);

        //soundboard
        Task<bool> CreateSoundboardSound(ulong senderId, ulong serverid, CreateSoundboardSoundRequestDTO requestDTO);
        Task<bool> EditSoundboardSound(ulong senderId, ulong serverid, ulong soundId, EditSoundboardSoundRequestDTO requestDTO);
        Task<bool> DeleteSoundboardSound(ulong senderId, ulong serverid, ulong soundId);

        //role
        Task<bool> CreateRole(ulong senderId, ulong serverid, CreateRoleRequestDTO requestDTO);
        Task<bool> EditRole(ulong senderId, ulong serverid, ulong roleId, EditRoleRequestDTO requestDTO);
        Task<bool> SetRolePermission(ulong senderId, ulong serverid, ulong roleId, SetPermissionStateRequestDTO requestDTO);
        Task<bool> SetRolePermissions(ulong senderId, ulong serverid, ulong roleId, SetMultiplePermissionStateRequestDTO requestDTO);
        Task<bool> DeleteRole(ulong senderId, ulong serverid, ulong roleId);
    }

}
