using AutoMapper;
using Azure.Core;
using CoreLib.DTO.RequestCore.InviteCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore;
using CoreLib.DTO.RequestCore.ServerCore.EmoteCore;
using CoreLib.DTO.RequestCore.ServerCore.Role;
using CoreLib.DTO.RequestCore.ServerCore.Server;
using CoreLib.DTO.RequestCore.ServerCore.SoundboardSound;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.Enums;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Bases;
using CoreLib.Interfaces.Services;
using DomainCoreApi.EFCORE;
using DomainCoreApi.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.Services
{
    public class ServerService : IServerService
    {
        private readonly EchoDbContext context;
        private readonly IMapper mapper;
        private readonly IPushNotificationService notificationService;
        private readonly IServerTextChannelService textChannelService;

        public ServerService(EchoDbContext context, IMapper mapper, IPushNotificationService notificationService, IServerTextChannelService textChannelService)
        {
            this.context = context;
            this.mapper = mapper;
            this.notificationService = notificationService;
            this.textChannelService = textChannelService;
        }

        public async Task<bool> ConsumeInvite(ulong userid, ulong serverid, InviteType type, string inviteCode)
        {
            //if (type == InviteType.Chat) { }
            throw new NotImplementedException();
        }

        public async Task<bool> CreateEmote(ulong userid, ulong serverid, CreateEmoteRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateEvent(ulong userid, ulong serverid, CreateEventRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateInvite(ulong userid, ulong serverid, InviteType type, CreateInviteRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateRole(ulong userid, ulong serverid, CreateRoleRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateServer(ulong userid, CreateServerRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateSoundboardSound(ulong userid, ulong serverid, CreateSoundboardSoundRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEmote(ulong userid, ulong serverid, ulong emoteId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEvent(ulong userid, ulong serverid, ulong eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteRole(ulong userid, ulong serverid, ulong roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteServer(ulong userid, ulong serverId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteSoundboardSound(ulong userid, ulong serverid, ulong soundId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditEmote(ulong userid, ulong serverid, EditEmoteRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditEvent(ulong userid, ulong serverid, EditEventRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditRole(ulong userid, ulong serverid, EditRoleRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditServer(ulong userid, ulong serverid, EditServerRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditSoundboardSound(ulong userid, ulong serverid, EditSoundboardSoundRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> JoinEvent(ulong userid, ulong serverid, ulong eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveEvent(ulong userid, ulong serverid, ulong eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LeaveServer(ulong userid, ulong serverId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MarkAsRead(ulong userid, ulong serverId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MuteServer(ulong userid, MuteRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveInvite(ulong userid, ulong serverid, InviteType type, string inviteCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetRolePermission(ulong userid, ulong serverid, SetPermissionStateRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetRolePermissions(ulong userid, ulong serverid, SetMultiplePermissionStateRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetServerImage(ulong userid, ulong serverid, SetImageRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> StartEvent(ulong userid, ulong serverid, ulong eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UnmuteServer(ulong userid, ulong serverId)
        {
            throw new NotImplementedException();
        }
    }
}
