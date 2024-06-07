using AutoMapper;
using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;
using CoreLib.Hubs;
using DomainPushNotificationApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Security.Principal;

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
                await _hubContext.Clients.Client(invite.Inviter.ToString()).ReceiveChatMinimalDTOCreateMessage(mapper.Map<ChatMinimalDTO>(invite));
            });

            _hubManager.Add((nameof(ChatInvite), EntityAction.Deleted), async (entity) =>
            {
                ChatInvite invite = entity as ChatInvite;
                await _hubContext.Clients.Client(invite.Inviter.ToString()).ReceiveChatMinimalDTODeleteMessage(mapper.Map<ChatMinimalDTO>(invite));
            });

            _hubManager.Add((nameof(OutgoingFriendRequest), EntityAction.Added), async (entity) =>
            {
                OutgoingFriendRequest outgoingFriendRequest = entity as OutgoingFriendRequest;
                await _hubContext.Clients.Client(outgoingFriendRequest.SenderId.ToString()).ReceiveUserMinimalDTOCreateMessage(mapper.Map<UserMinimalDTO>(outgoingFriendRequest));
            });

            _hubManager.Add((nameof(OutgoingFriendRequest), EntityAction.Deleted), async (entity) =>
            {
                OutgoingFriendRequest outgoingFriendRequest = entity as OutgoingFriendRequest;
                await _hubContext.Clients.Client(outgoingFriendRequest.SenderId.ToString()).ReceiveUserMinimalDTODeleteMessage(mapper.Map<UserMinimalDTO>(outgoingFriendRequest));
            });

            _hubManager.Add((nameof(IncomingFriendRequest), EntityAction.Added), async (entity) =>
            {
                IncomingFriendRequest incomingFriendRequest = entity as IncomingFriendRequest;
                await _hubContext.Clients.Client(incomingFriendRequest.ReceiverId.ToString()).ReceiveUserMinimalDTOCreateMessage(mapper.Map<UserMinimalDTO>(incomingFriendRequest));
            });

            _hubManager.Add((nameof(IncomingFriendRequest), EntityAction.Deleted), async (entity) =>
            {
                IncomingFriendRequest incomingFriendRequest = entity as IncomingFriendRequest;
                await _hubContext.Clients.Client(incomingFriendRequest.ReceiverId.ToString()).ReceiveUserMinimalDTODeleteMessage(mapper.Map<UserMinimalDTO>(incomingFriendRequest));
            });

            _hubManager.Add((nameof(Friendship), EntityAction.Added), async (entity) =>
            {
                Friendship friendship = entity as Friendship;
                foreach (var participant in friendship.Participants)
                {
                    await _hubContext.Clients.User(participant.ParticipantId.ToString()).NewFriend(mapper.Map<UserDTO>(friendship.Participants.First(e => e.ParticipantId != participant.SubjectId)));
                }
            });

            _hubManager.Add((nameof(Friendship), EntityAction.Deleted), async (entity) =>
            {
                Friendship friendship = entity as Friendship;
                foreach (var participant in friendship.Participants)
                {
                    await _hubContext.Clients.User(participant.ParticipantId.ToString()).RemoveFriend(mapper.Map<UserDTO>(friendship.Participants.First(e => e.ParticipantId != participant.SubjectId)));
                }
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Added), async (entity) =>
            {
                AccountBlock nickname = entity as AccountBlock;
                await _hubContext.Clients.Client(nickname.BlockerId.ToString()).ReceiveUserFullDTOCreateMessage(mapper.Map<UserFullDTO>(nickname));

                //ved ikke helt hvad vi skal send til den som bliver block
                await _hubContext.Clients.Client(nickname.BlockedId.ToString()).ReceiveUserFullDTOCreateMessage(mapper.Map<UserFullDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Modified), async (entity) =>
            {
                AccountBlock nickname = entity as AccountBlock;
                await _hubContext.Clients.Client(nickname.BlockerId.ToString()).ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(nickname));

                //ved ikke helt hvad vi skal send til den som bliver block
                await _hubContext.Clients.Client(nickname.BlockedId.ToString()).ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Deleted), async (entity) =>
            {
                AccountBlock nickname = entity as AccountBlock;
                await _hubContext.Clients.Client(nickname.BlockerId.ToString()).ReceiveUserFullDTODeleteMessage(mapper.Map<UserFullDTO>(nickname));

                //ved ikke helt hvad vi skal send til den som bliver block
                await _hubContext.Clients.Client(nickname.BlockedId.ToString()).ReceiveUserFullDTODeleteMessage(mapper.Map<UserFullDTO>(nickname));
            });

            _hubManager.Add((nameof(VoiceSettings), EntityAction.Modified), async (entity) =>
            {
                VoiceSettings voiceSettings = entity as VoiceSettings;
                await _hubContext.Clients.Client(voiceSettings.AccountSettings.Account.Id.ToString()).ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(voiceSettings));
            });

            _hubManager.Add((nameof(User), EntityAction.Modified), async (entity) =>
            {
                User user = entity as User;
                await _hubContext.Clients.All.ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(user));
                await _hubContext.Clients.All.ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(user));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(user));
                await _hubContext.Clients.All.ReceiveUserProfileDTOUpdateMessage(mapper.Map<UserProfileDTO>(user));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(user));
                await _hubContext.Clients.All.ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(user));
            });

            _hubManager.Add((nameof(User), EntityAction.Deleted), async (entity) =>
            {
                User user = entity as User;
                await _hubContext.Clients.All.ReceiveUserDTODeleteMessage(mapper.Map<UserDTO>(user));
                await _hubContext.Clients.All.ReceiveUserFullDTODeleteMessage(mapper.Map<UserFullDTO>(user));
                await _hubContext.Clients.All.ReceiveUserMinimalDTODeleteMessage(mapper.Map<UserMinimalDTO>(user));
                await _hubContext.Clients.All.ReceiveUserProfileDTODeleteMessage(mapper.Map<UserProfileDTO>(user));
                await _hubContext.Clients.All.ReceiveUserMinimalDTODeleteMessage(mapper.Map<UserMinimalDTO>(user));
                await _hubContext.Clients.All.ReceiveMemberDTODeleteMessage(mapper.Map<MemberDTO>(user));
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

                await _hubContext.Clients.Users(chat.Participants.Select(e => e.ParticipantId.ToString())).ReceiveChatDTOUpdateMessage(mapper.Map<ChatDTO>(chat));

            });

            _hubManager.Add((nameof(Chat), EntityAction.Deleted), async (entity) =>
            {
                Chat chat = entity as Chat;

                await _hubContext.Clients.Users(chat.Participants.Select(e => e.ParticipantId.ToString())).ReceiveChatDTODeleteMessage(mapper.Map<ChatDTO>(chat));

            });

            _hubManager.Add((nameof(Account), EntityAction.Modified), async (entity) =>
            {
                Account account = entity as Account;
                await _hubContext.Clients.All.ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(account));
                await _hubContext.Clients.All.ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveUserProfileDTOUpdateMessage(mapper.Map<UserProfileDTO>(account));
                await _hubContext.Clients.All.ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(account));
            });

            _hubManager.Add((nameof(AccountProfile), EntityAction.Modified), async (entity) =>
            {
                AccountProfile account = entity as AccountProfile;
                await _hubContext.Clients.All.ReceiveUserDTOUpdateMessage(mapper.Map<UserDTO>(account));
                await _hubContext.Clients.All.ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveUserProfileDTOUpdateMessage(mapper.Map<UserProfileDTO>(account));
                await _hubContext.Clients.All.ReceiveUserMinimalDTOUpdateMessage(mapper.Map<UserMinimalDTO>(account));
                await _hubContext.Clients.All.ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(account));
            });

            #region snakke med Jas

            _hubManager.Add((nameof(AccountNickname), EntityAction.Added), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserFullDTOCreateMessage(mapper.Map<UserFullDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Modified), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Deleted), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;
                await _hubContext.Clients.Client(nickname.AuthorId.ToString()).ReceiveUserFullDTODeleteMessage(mapper.Map<UserFullDTO>(nickname));
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Added), async (entity) =>
            {
                AccountNote note = entity as AccountNote;
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserFullDTOCreateMessage(mapper.Map<UserFullDTO>(note));
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Modified), async (entity) =>
            {
                AccountNote note = entity as AccountNote;
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserFullDTOUpdateMessage(mapper.Map<UserFullDTO>(note));
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Deleted), async (entity) =>
            {
                AccountNote note = entity as AccountNote;
                await _hubContext.Clients.Client(note.AuthorId.ToString()).ReceiveUserFullDTODeleteMessage(mapper.Map<UserFullDTO>(note));
            });

            _hubManager.Add((nameof(ChatMute), EntityAction.Added), async (entity) =>
            {
                ChatMute chatMute = entity as ChatMute;
            });

            _hubManager.Add((nameof(ChatMute), EntityAction.Deleted), async (entity) =>
            {
                ChatMute chatMute = entity as ChatMute;
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Added), async (entity) =>
            {
                ChatParticipancy message = entity as ChatParticipancy;
                await _hubContext.Clients.User(message.ParticipantId.ToString()).ReceiveMemberDTOCreateMessage(mapper.Map<MemberDTO>(message));

                await _hubContext.Clients.Users(message.Subject.Participants.Select(e => e.ParticipantId.ToString())).ReceiveMemberDTOCreateMessage(mapper.Map<MemberDTO>(message));

                //await _hubContext.Clients.Group($"{typeof(ChatMessage)}/{message.Id}").ReceiveMessageDTOCreateMessage(mapper.Map<MessageDTO>(message));

            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Modified), async (entity) =>
            {
                ChatParticipancy message = entity as ChatParticipancy;
                await _hubContext.Clients.User(message.ParticipantId.ToString()).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(message));

                await _hubContext.Clients.Users(message.Subject.Participants.Select(e => e.ParticipantId.ToString())).ReceiveMemberDTOUpdateMessage(mapper.Map<MemberDTO>(message));
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Deleted), async (entity) =>
            {
                ChatParticipancy message = entity as ChatParticipancy;
                await _hubContext.Clients.User(message.ParticipantId.ToString()).ReceiveMemberDTODeleteMessage(mapper.Map<MemberDTO>(message));

                await _hubContext.Clients.Users(message.Subject.Participants.Select(e => e.ParticipantId.ToString())).ReceiveMemberDTODeleteMessage(mapper.Map<MemberDTO>(message));
            });
            #endregion


        }

        public async Task NotifyClients(DomainEvent domain)
        {
            await Task.Run(async () =>
            {
                if (_hubManager.TryGetValue((domain.Type, domain.Action), out var task))
                {
                    await task(domain.Entity);
                }
            });
        }
    }
}

//chat
/* chatdto */

//chatmessage
/* messagedto
 * chat */

//chatparticipancy
/* memberdto
 * chatdto */

// accountvolume
/* memberDto */

//accountmute
/* memberDto */

//user
/* UserDTO
 * UserFullDTO
 * UserMinimalDTO
 * UserProfileDTO
 * MemberDTO */

//voicesettings
/* UserFullDTO
 * VoiceSettingsDTO igennem userfull */

//accountnote
/* UserFullDTO
 * MemberDTO */

//accountnickname
/* UserFullDTO
 * MemberDTO
 * UserProfileDTO */

//accountblock
/* UserFullDTO
 * UserMinimalDTO? */

//friendship 
/* UserFullDTO 
 * FriendRequestDTO*/

//incomingrequest
/* UserFullDTO 
 * FriendRequestDTO*/

//outgoingrequest
/* UserFullDTO 
 * FriendRequestDTO*/

//chatmute
/* UserFullDTO
 * chatdto */

//chataccountmessagetracker
/* UserFullDTO
 * chatdto*/

//chatinvite
/* cahtdto
 * InviteDTO
 * InviteMinimalDTO
 * UserFullDTO */