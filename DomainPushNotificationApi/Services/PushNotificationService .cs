using AutoMapper;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.EchoCore.UserCore.SettingsCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;
using CoreLib.Hubs;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Contracts;
using DomainPushNotificationApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DomainPushNotificationApi.Services
{
    public class PushNotificationService
    {
        protected readonly IHubContext<PushNotificationHub, IPushNotificationHub> _hubContext;
        private Dictionary<(string,EntityAction), Func<object,Task>> _hubManager = new();
        public PushNotificationService(IHubContext<PushNotificationHub, IPushNotificationHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;

            _hubManager.Add((nameof(ChatInvite), EntityAction.Added), async (entity) =>
            {
                ChatInvite invite = entity as ChatInvite;
                await _hubContext.Clients.Group($"{typeof(Chat)}/{invite.SubjectId}").ReceiveInviteDTOCreateMessage(mapper.Map<InviteDTO>(invite));
            });

            _hubManager.Add((nameof(ChatInvite), EntityAction.Deleted), async (entity) =>
            {
                ChatInvite invite = entity as ChatInvite;
                await _hubContext.Clients.Group($"{typeof(Chat)}/{invite.SubjectId}").ReceiveInviteDTODeleteMessage(mapper.Map<InviteDTO>(invite));
            });

            _hubManager.Add((nameof(OutgoingFriendRequest), EntityAction.Added), async (entity) =>
            {
                OutgoingFriendRequest outgoingFriendRequest = entity as OutgoingFriendRequest;
                await _hubContext.Clients.Client(outgoingFriendRequest.SenderId.ToString()).ReceiveFriendRequestDTOCreateMessage(mapper.Map<FriendRequestDTO>(outgoingFriendRequest));
            });

            _hubManager.Add((nameof(OutgoingFriendRequest), EntityAction.Deleted), async (entity) =>
            {
                OutgoingFriendRequest outgoingFriendRequest = entity as OutgoingFriendRequest;
                await _hubContext.Clients.Client(outgoingFriendRequest.SenderId.ToString()).ReceiveFriendRequestDTODeleteMessage(mapper.Map<FriendRequestDTO>(outgoingFriendRequest));
            });

            _hubManager.Add((nameof(IncomingFriendRequest), EntityAction.Added), async (entity) =>
            {
                IncomingFriendRequest incomingFriendRequest = entity as IncomingFriendRequest;
                await _hubContext.Clients.Client(incomingFriendRequest.ReceiverId.ToString()).ReceiveFriendRequestDTOCreateMessage(mapper.Map<FriendRequestDTO>(incomingFriendRequest));
            });

            _hubManager.Add((nameof(IncomingFriendRequest), EntityAction.Deleted), async (entity) =>
            {
                IncomingFriendRequest incomingFriendRequest = entity as IncomingFriendRequest;
                await _hubContext.Clients.Client(incomingFriendRequest.ReceiverId.ToString()).ReceiveFriendRequestDTODeleteMessage(mapper.Map<FriendRequestDTO>(incomingFriendRequest));
            });

            _hubManager.Add((nameof(Friendship), EntityAction.Added), async (entity) =>
            {
                Friendship friendship = entity as Friendship;
                foreach (var participant in friendship.Participants)
                {
                    await _hubContext.Clients.User(participant.ParticipantId.ToString()).NewFriend(mapper.Map<UserDTO>(friendship.Participants.First(e => e.ParticipantId != participant.ParticipantId)));
                }
            });

            _hubManager.Add((nameof(Friendship), EntityAction.Deleted), async (entity) =>
            {
                Friendship friendship = entity as Friendship;
                foreach (var participant in friendship.Participants)
                {
                    await _hubContext.Clients.User(participant.ParticipantId.ToString()).RemoveFriend(mapper.Map<UserDTO>(friendship.Participants.First(e => e.ParticipantId != participant.ParticipantId)));
                }
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Added), async (entity) =>
            {
                AccountBlock block = entity as AccountBlock;
                await _hubContext.Clients.Client(block.BlockerId.ToString()).ReceiveUserMinimalDTOCreateMessage(mapper.Map<UserMinimalDTO>(block));
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Deleted), async (entity) =>
            {
                AccountBlock block = entity as AccountBlock;
                await _hubContext.Clients.Client(block.BlockerId.ToString()).ReceiveUserMinimalDTODeleteMessage(mapper.Map<UserMinimalDTO>(block));
            });

            _hubManager.Add((nameof(VoiceSettings), EntityAction.Modified), async (entity) =>
            {
                VoiceSettings voiceSettings = entity as VoiceSettings;
                await _hubContext.Clients.Client(voiceSettings.AccountSettings.Account.Id.ToString()).ReceiveVoiceSettingsDTOUpdateMessage(mapper.Map<VoiceSettingsDTO>(voiceSettings));
                //Note: RTC skal også have dette at vide hvis vi når det
            });

            _hubManager.Add((nameof(User), EntityAction.Modified), async (entity) =>
            {
                User user = entity as User;
                //Det kun Email og phone nr, så derfor lave DTO senere
                await _hubContext.Clients.User(user.Account.Id.ToString()).ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(user));
            });

            _hubManager.Add((nameof(User), EntityAction.Deleted), async (entity) =>
            {
                User user = entity as User;
                //note skal måske ikke være her for bruger skal bare logges ud efter de har slette deres bruger
                await _hubContext.Clients.User(user.Account.Id.ToString()).ReceiveUserFullDTODeleteMessage(mapper.Map<UserFullDTO>(user));
            });
 
            _hubManager.Add((nameof(ChatMessage), EntityAction.Added), async (entity) =>
            {
                ChatMessage message = entity as ChatMessage;
                await _hubContext.Clients.Group($"{typeof(ChatMessage)}/{message.Id}").ReceiveMessageDTOCreateMessage(mapper.Map<MessageDTO>(message));

            });

            _hubManager.Add((nameof(ChatMessage), EntityAction.Modified), async (entity) =>
            {
                ChatMessage message = entity as ChatMessage;
                await _hubContext.Clients.Group($"{typeof(ChatMessage)}/{message.Id}").ReceiveMessageDTOUpdateMessage(mapper.Map<MessageDTO>(message));

            });

            _hubManager.Add((nameof(ChatMessage), EntityAction.Deleted), async (entity) =>
            {
                ChatMessage message = entity as ChatMessage;
                await _hubContext.Clients.Group($"{typeof(ChatMessage)}/{message.Id}").ReceiveMessageDTODeleteMessage(mapper.Map<MessageDTO>(message));

            });

            _hubManager.Add((nameof(Chat), EntityAction.Added), async (entity) =>
            {
                Chat chat = entity as Chat;

                await _hubContext.Clients.Users(chat.Participants.Select(e => e.ParticipantId.ToString())).ReceiveChatDTOCreateMessage(mapper.Map<ChatDTO>(chat));

            });

            _hubManager.Add((nameof(Chat), EntityAction.Modified), async (entity) =>
            {
                Chat chat = entity as Chat;

                await _hubContext.Clients.Group($"{typeof(ChatMessage)}/{chat.Id}").ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(chat));
            });

            //Note skal muligvis ikke bruges da vi skal finder ud af om vi sletter chat direkte
            _hubManager.Add((nameof(Chat), EntityAction.Deleted), async (entity) =>
            {
                Chat chat = entity as Chat;

                await _hubContext.Clients.Users(chat.Participants.Select(e => e.ParticipantId.ToString())).ReceiveChatDTODeleteMessage(mapper.Map<ChatDTO>(chat));

            });

            //Note der er svær logik så lave senere hvis der tid
            _hubManager.Add((nameof(Account), EntityAction.Modified), async (entity) =>
            {
                Account account = entity as Account;
                await _hubContext.Clients.All.ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(account));
                await _hubContext.Clients.All.ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveUserProfileDTOUpdateMessage(mapper.Map<UserProfileDTO>(account));
                await _hubContext.Clients.All.ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(account));
            });

            //Note der er svær logik så lave senere hvis der tid
            _hubManager.Add((nameof(AccountProfile), EntityAction.Modified), async (entity) =>
            {
                AccountProfile account = entity as AccountProfile;
                await _hubContext.Clients.All.ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(account));
                await _hubContext.Clients.All.ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveUserProfileDTOUpdateMessage(mapper.Map<UserProfileDTO>(account));
                await _hubContext.Clients.All.ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(account));
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Added), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(nickname));
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(nickname));
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Modified), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(nickname));
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(nickname));
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Deleted), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(nickname));
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(nickname));
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Added), async (entity) =>
            {
                AccountNote note = entity as AccountNote;
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(note));
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(note));
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(note));
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Modified), async (entity) =>
            {
                AccountNote note = entity as AccountNote;
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(note));
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(note));
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(note));
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Deleted), async (entity) =>
            {
                AccountNote note = entity as AccountNote;
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(note));
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(note));
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(note));
            });

            _hubManager.Add((nameof(ChatMute), EntityAction.Added), async (entity) =>
            {
                ChatMute chatMute = entity as ChatMute;
                await _hubContext.Clients.User(chatMute.MuterId.ToString()).ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(chatMute.Subject));
            });

            _hubManager.Add((nameof(ChatMute), EntityAction.Deleted), async (entity) =>
            {
                ChatMute chatMute = entity as ChatMute;
                await _hubContext.Clients.User(chatMute.MuterId.ToString()).ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(chatMute.Subject));
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Added), async (entity) =>
            {
                ChatParticipancy participancy = entity as ChatParticipancy;
                await _hubContext.Clients.Group($"{typeof(Chat)}/{participancy.SubjectId}").ReceiveMemberDTOCreateMessage(mapper.Map<MemberDTO>(participancy));
                await _hubContext.Clients.User(participancy.ParticipantId.ToString()).ReceiveChatDTOCreateMessage(mapper.Map<ChatDTO>(participancy));

            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Modified), async (entity) =>
            {
                ChatParticipancy participancy = entity as ChatParticipancy;
                await _hubContext.Clients.Group($"{typeof(Chat)}/{participancy.SubjectId}").ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(participancy));
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Deleted), async (entity) =>
            {
                ChatParticipancy participancy = entity as ChatParticipancy;
                await _hubContext.Clients.User(participancy.ParticipantId.ToString()).LeaveChat(mapper.Map<ChatDTO>(participancy));

                await _hubContext.Clients.Group($"{typeof(Chat)}/{participancy.SubjectId}").ReceiveMemberDTODeleteMessage(mapper.Map<MemberDTO>(participancy));

            });

            _hubManager.Add((nameof(ChatAccountMessageTracker), EntityAction.Added), async (entity) =>
            {
                ChatAccountMessageTracker tracker = entity as ChatAccountMessageTracker;
                await _hubContext.Clients.User(tracker.OwnerId.ToString()).ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(tracker));

            });

            _hubManager.Add((nameof(ChatAccountMessageTracker), EntityAction.Modified), async (entity) =>
            {
                ChatAccountMessageTracker tracker = entity as ChatAccountMessageTracker;
                await _hubContext.Clients.User(tracker.OwnerId.ToString()).ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(tracker));

            });

            _hubManager.Add((nameof(ChatAccountMessageTracker), EntityAction.Deleted), async (entity) =>
            {
                ChatAccountMessageTracker tracker = entity as ChatAccountMessageTracker;
                await _hubContext.Clients.User(tracker.OwnerId.ToString()).ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(tracker));

            });

            _hubManager.Add((nameof(AccountAccountVolume), EntityAction.Added), async (entity) =>
            {
                AccountAccountVolume volume = entity as AccountAccountVolume;
                await _hubContext.Clients.Client(volume.OwnerId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(volume));
            });

            _hubManager.Add((nameof(AccountAccountVolume), EntityAction.Modified), async (entity) =>
            {
                AccountAccountVolume volume = entity as AccountAccountVolume;
                await _hubContext.Clients.Client(volume.OwnerId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(volume));
            });

            _hubManager.Add((nameof(AccountAccountVolume), EntityAction.Deleted), async (entity) =>
            {
                AccountAccountVolume volume = entity as AccountAccountVolume;
                await _hubContext.Clients.Client(volume.OwnerId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(volume));
            });

            _hubManager.Add((nameof(AccountMute), EntityAction.Added), async (entity) =>
            {
                AccountMute mute = entity as AccountMute;
                await _hubContext.Clients.Client(mute.MuterId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(mute.Subject));
            });

            _hubManager.Add((nameof(AccountMute), EntityAction.Deleted), async (entity) =>
            {
                AccountMute mute = entity as AccountMute;
                await _hubContext.Clients.Client(mute.MuterId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(mute.Subject));
            });
        }

        public async Task NotifyClients(DomainEvent domain)
        {
            var entity = JsonSerializer.Deserialize(domain.Entity, Type.GetType(domain.Type), new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve } );
            //var ename = entity.GetType().Name;
            //if (_hubManager.TryGetValue((ename, domain.Action), out var task))
            //{
            //    await task(entity);
            //}
            await Task.Run(async () =>
            {
                if (_hubManager.TryGetValue((entity.GetType().Name, domain.Action), out var task))
                {
                    await task(entity);
                }
            });
        }
    }
}