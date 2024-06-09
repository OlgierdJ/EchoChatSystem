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
using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DomainPushNotificationApi.Services
{
    public class PushNotificationService
    {
        protected readonly IHubContext<PushNotificationHub, IPushNotificationHub> _hubContext;
        private Dictionary<(string, EntityAction), Func<object, Task>> _hubManager = new();
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve };
        public PushNotificationService(IHubContext<PushNotificationHub, IPushNotificationHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;

            _hubManager.Add((nameof(ChatInvite), EntityAction.Added), async (entity) =>
            {
                ChatInvite invite = entity as ChatInvite;
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{invite.SubjectId}")
                .ChatInviteAdded(invite.SubjectId, mapper.Map<InviteMinimalDTO>(invite));
            });

            _hubManager.Add((nameof(ChatInvite), EntityAction.Deleted), async (entity) =>
            {
                ChatInvite invite = entity as ChatInvite;
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{invite.SubjectId}")
                .ChatInviteRemoved(invite.SubjectId, invite.InviteCode);
            });

            _hubManager.Add((nameof(OutgoingFriendRequest), EntityAction.Added), async (entity) =>
            {
                OutgoingFriendRequest outgoingFriendRequest = entity as OutgoingFriendRequest;
                //hopefully account, profile, activestatus and customstatus is included in context
                var map = mapper.Map<FriendRequestDTO>(outgoingFriendRequest);
                await _hubContext.Clients
                .User(outgoingFriendRequest.SenderId.ToString())
                .FriendRequestAdded(map);
            });

            _hubManager.Add((nameof(OutgoingFriendRequest), EntityAction.Deleted), async (entity) =>
            {
                OutgoingFriendRequest outgoingFriendRequest = entity as OutgoingFriendRequest;
                await _hubContext.Clients
                .User(outgoingFriendRequest.SenderId.ToString())
                .FriendRequestRemoved(RequestType.Outgoing, outgoingFriendRequest.Id);
            });

            _hubManager.Add((nameof(IncomingFriendRequest), EntityAction.Added), async (entity) =>
            {
                IncomingFriendRequest incomingFriendRequest = entity as IncomingFriendRequest;
                //hopefully account, profile, activestatus and customstatus is included in context
                await _hubContext.Clients
                .User(incomingFriendRequest.ReceiverId.ToString())
                .FriendRequestAdded(mapper.Map<FriendRequestDTO>(incomingFriendRequest));
            });

            _hubManager.Add((nameof(IncomingFriendRequest), EntityAction.Deleted), async (entity) =>
            {
                IncomingFriendRequest incomingFriendRequest = entity as IncomingFriendRequest;
                await _hubContext.Clients
                .User(incomingFriendRequest.ReceiverId.ToString())
                .FriendRequestRemoved(RequestType.Incoming, incomingFriendRequest.Id);
            });

            _hubManager.Add((nameof(Friendship), EntityAction.Added), async (entity) =>
            {
                Friendship friendship = entity as Friendship;
                //hopefully account, profile, activestatus and customstatus is included from context.
                foreach (var participant in friendship.Participants)
                {
                    await _hubContext.Clients
                    .User(participant.ParticipantId.ToString())
                    .FriendAdded(mapper.Map<UserDTO>(friendship.Participants.First(e => e.ParticipantId != participant.ParticipantId)));
                }
            });

            _hubManager.Add((nameof(Friendship), EntityAction.Deleted), async (entity) =>
            {
                Friendship friendship = entity as Friendship;
                foreach (var participant in friendship.Participants)
                {
                    await _hubContext.Clients
                    .User(participant.ParticipantId.ToString())
                    .FriendRemoved(friendship.Participants.First(e => e.ParticipantId != participant.ParticipantId).ParticipantId);
                }
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Added), async (entity) =>
            {
                AccountBlock block = entity as AccountBlock;
                //hopefully we got account and profile here from context.
                await _hubContext.Clients
                .User(block.BlockerId.ToString())
                .BlockedUserAdded(mapper.Map<UserMinimalDTO>(block));
            });

            _hubManager.Add((nameof(AccountBlock), EntityAction.Deleted), async (entity) =>
            {
                AccountBlock block = entity as AccountBlock;
                await _hubContext.Clients
                .User(block.BlockerId.ToString())
                .BlockedUserRemoved(block.BlockedId);
            });

            _hubManager.Add((nameof(VoiceSettings), EntityAction.Modified), async (entity) =>
            {
                VoiceSettings voiceSettings = entity as VoiceSettings;
                //voicesettingsid should be the same as
                //accountsettingsid which should be
                //the same as account id
                await _hubContext.Clients
                .User(voiceSettings.Id.ToString())
                .VoiceSettingsUpdated(mapper.Map<VoiceSettingsDTO>(voiceSettings));
                //Note: RTC skal også have dette at vide hvis vi når det
            });

            _hubManager.Add((nameof(User), EntityAction.Modified), async (entity) =>
            {
                User user = entity as User;
                //Det kun Email og phone nr, så derfor lave DTO senere
                await _hubContext.Clients
                .User(user.Account.Id.ToString())
                .SensitiveDataUpdated(user.Email, user.PhoneNumber); //maybe move to dto and map it properly.
            });

            _hubManager.Add((nameof(User), EntityAction.Deleted), async (entity) =>
            {
                User user = entity as User;
                //note skal måske ikke være her for bruger skal bare logges ud efter de har slette deres bruger
                await _hubContext.Clients
                .User(user.Account.Id.ToString())
                .ForceLogout();
            });

            _hubManager.Add((nameof(ChatMessage), EntityAction.Added), async (entity) =>
            {
                ChatMessage message = entity as ChatMessage;
                //hopefully author is included else is systemmessage
                await _hubContext.Clients
                .Group($"{nameof(Chat)}/{message.MessageHolderId}")
                .ChatMessageAdded(message.MessageHolderId, mapper.Map<MessageDTO>(message));

            });

            _hubManager.Add((nameof(ChatMessage), EntityAction.Modified), async (entity) =>
            {
                ChatMessage message = entity as ChatMessage;
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{message.MessageHolderId}")
                .ChatMessageUpdated(message.MessageHolderId, message.Id, message.Content, message.TimeEdited);

            });

            _hubManager.Add((nameof(ChatMessage), EntityAction.Deleted), async (entity) =>
            {
                ChatMessage message = entity as ChatMessage;
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{message.MessageHolderId}")
                .ChatMessageRemoved(message.MessageHolderId, message.Id);

            });

            _hubManager.Add((nameof(ChatMessagePin), EntityAction.Added), async (entity) =>
            {
                ChatMessagePin pin = entity as ChatMessagePin;
                //hopefully author is included else is systemmessage
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{pin.PinboardId}")
                .ChatMessagePinAdded(pin.PinboardId, pin.MessageId);

            });

            _hubManager.Add((nameof(ChatMessagePin), EntityAction.Deleted), async (entity) =>
            {
                ChatMessagePin pin = entity as ChatMessagePin;
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{pin.PinboardId}")
                .ChatMessagePinRemoved(pin.PinboardId, pin.MessageId);

            });

            //dont think we need this one ??
            //_hubManager.Add((nameof(Chat), EntityAction.Added), async (entity) =>
            //{
            //    Chat chat = entity as Chat;

            //    await _hubContext.Clients
            //    .Users(chat.Participants.Select(e => e.ParticipantId.ToString()))
            //    .ChatAdded(mapper.Map<ChatDTO>(chat));

            //});

            _hubManager.Add((nameof(Chat), EntityAction.Modified), async (entity) =>
            {
                Chat chat = entity as Chat;

                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{chat.Id}")
                .ChatUpdated(chat.Id, chat.Name, chat.IconUrl);
            });

            //Note skal muligvis ikke bruges da vi skal finder ud af om vi sletter chat direkte
            //_hubManager.Add((nameof(Chat), EntityAction.Deleted), async (entity) =>
            //{
            //    Chat chat = entity as Chat;

            //    await _hubContext.Clients
            //    .Group($"{typeof(Chat)}/{chat.Id}")
            //    .ChatRemoved(chat.Id);

            //});

            //Note der er svær logik så lave senere hvis der tid
            _hubManager.Add((nameof(Account), EntityAction.Modified), async (entity) =>
            {
                Account account = entity as Account;
                
                await _hubContext.Clients.All
               .UserActivityStatusChanged(account.Id, account.ActivityStatusId, account.CustomStatus?.CustomMessage);
            });

            //Note der er svær logik så lave senere hvis der tid
            _hubManager.Add((nameof(AccountProfile), EntityAction.Modified), async (entity) =>
            {
                AccountProfile profile = entity as AccountProfile;
                
                await _hubContext.Clients.All
               .UserProfileChanged(profile.AccountId, profile.DisplayName, profile.AvatarFileURL, profile.About, profile.BannerColor);
               //.User(profile.AccountId.ToString())
              //  await _hubContext.Clients.All
              ////.User(profile.AccountId.ToString())
              //.ExternalImageChanged(profile.AccountId, profile.AvatarFileURL);
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Added), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;

                await _hubContext.Clients
                .User(nickname.AuthorId.ToString())
                .ExternalUserNameChanged(nickname.SubjectId, nickname.Nickname);
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Modified), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;

                await _hubContext.Clients
                .User(nickname.AuthorId.ToString())
                .ExternalUserNameChanged(nickname.SubjectId, nickname.Nickname);
            });

            _hubManager.Add((nameof(AccountNickname), EntityAction.Deleted), async (entity) =>
            {
                AccountNickname nickname = entity as AccountNickname;

                //hopefully we got a bit of context here to set original name
                //else maybe just prompt user retrieve name via api or something thats just longer
                var name = nickname.Subject.Profile == null ? nickname.Subject.Name : nickname.Subject.Profile.DisplayName;

                await _hubContext.Clients
                .User(nickname.AuthorId.ToString())
                .ExternalUserNameChanged(nickname.SubjectId, name);
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Added), async (entity) =>
            {
                AccountNote note = entity as AccountNote;

                await _hubContext.Clients
                .User(note.AuthorId.ToString())
                .ExternalUserNoteChanged(note.SubjectId, note.Note);
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Modified), async (entity) =>
            {
                AccountNote note = entity as AccountNote;

                await _hubContext.Clients
                .User(note.AuthorId.ToString())
                .ExternalUserNoteChanged(note.SubjectId, note.Note);
            });

            _hubManager.Add((nameof(AccountNote), EntityAction.Deleted), async (entity) =>
            {
                AccountNote note = entity as AccountNote;

                await _hubContext.Clients
                .User(note.AuthorId.ToString())
                .ExternalUserNoteChanged(note.SubjectId, null); //default note state
            });

            _hubManager.Add((nameof(ChatMute), EntityAction.Added), async (entity) =>
            {
                ChatMute chatMute = entity as ChatMute;
                await _hubContext.Clients
                .User(chatMute.MuterId.ToString())
                .ChatMutedStateChanged(chatMute.SubjectId, true);
            });

            _hubManager.Add((nameof(ChatMute), EntityAction.Deleted), async (entity) =>
            {
                ChatMute chatMute = entity as ChatMute;
                await _hubContext.Clients
                .User(chatMute.MuterId.ToString())
                .ChatMutedStateChanged(chatMute.SubjectId, false);
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Added), async (entity) =>
            {
                ChatParticipancy participancy = entity as ChatParticipancy;

                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{participancy.SubjectId}")
                .ChatMemberJoined(participancy.SubjectId, mapper.Map<MemberDTO>(participancy));

                await _hubContext.Clients
                .User(participancy.ParticipantId.ToString())
                .ChatJoined(mapper.Map<ChatDTO>(participancy));
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Modified), async (entity) =>
            {
                ChatParticipancy participancy = entity as ChatParticipancy;
                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{participancy.SubjectId}")
                .ChatMemberOwnershipChanged(participancy.SubjectId, participancy.ParticipantId, participancy.IsOwner);

                await _hubContext.Clients
                .User(participancy.ParticipantId.ToString())
                .ChatHiddenStateChanged(participancy.SubjectId, participancy.Hidden);
            });

            _hubManager.Add((nameof(ChatParticipancy), EntityAction.Deleted), async (entity) =>
            {
                ChatParticipancy participancy = entity as ChatParticipancy;
                await _hubContext.Clients
                .User(participancy.ParticipantId.ToString())
                .LeftChat(participancy.SubjectId);

                await _hubContext.Clients
                .Group($"{typeof(Chat)}/{participancy.SubjectId}")
                .ChatMemberLeft(participancy.SubjectId,participancy.ParticipantId);

            });

            _hubManager.Add((nameof(ChatAccountMessageTracker), EntityAction.Added), async (entity) =>
            {
                ChatAccountMessageTracker tracker = entity as ChatAccountMessageTracker;
                await _hubContext.Clients
                .User(tracker.OwnerId.ToString())
                .ChatLastReadMessageChanged(tracker.CoOwnerId, tracker.SubjectId);

            });

            _hubManager.Add((nameof(ChatAccountMessageTracker), EntityAction.Modified), async (entity) =>
            {
                ChatAccountMessageTracker tracker = entity as ChatAccountMessageTracker;
                await _hubContext.Clients
                .User(tracker.OwnerId.ToString())
                .ChatLastReadMessageChanged(tracker.CoOwnerId, tracker.SubjectId);

            });

            _hubManager.Add((nameof(ChatAccountMessageTracker), EntityAction.Deleted), async (entity) =>
            {
                ChatAccountMessageTracker tracker = entity as ChatAccountMessageTracker;
                await _hubContext.Clients
                .User(tracker.OwnerId.ToString())
                .ChatLastReadMessageChanged(tracker.CoOwnerId, null);

            });

            _hubManager.Add((nameof(AccountAccountVolume), EntityAction.Added), async (entity) =>
            {
                AccountAccountVolume volume = entity as AccountAccountVolume;
                await _hubContext.Clients
                .User(volume.OwnerId.ToString())
                .ExternalUserVolumeChanged(volume.SubjectId, volume.Volume);
            });

            _hubManager.Add((nameof(AccountAccountVolume), EntityAction.Modified), async (entity) =>
            {
                AccountAccountVolume volume = entity as AccountAccountVolume;
                await _hubContext.Clients
                .User(volume.OwnerId.ToString())
                .ExternalUserVolumeChanged(volume.SubjectId, volume.Volume);
            });

            _hubManager.Add((nameof(AccountAccountVolume), EntityAction.Deleted), async (entity) =>
            {
                AccountAccountVolume volume = entity as AccountAccountVolume;
                await _hubContext.Clients.User(volume.OwnerId.ToString()).ExternalUserVolumeChanged(volume.SubjectId, 100); //default volume
            });

            _hubManager.Add((nameof(AccountMute), EntityAction.Added), async (entity) =>
            {
                AccountMute mute = entity as AccountMute;
                await _hubContext.Clients
                .User(mute.MuterId.ToString())
                .UserMutedStateChanged(mute.SubjectId, true);
            });

            _hubManager.Add((nameof(AccountMute), EntityAction.Deleted), async (entity) =>
            {
                AccountMute mute = entity as AccountMute;
                await _hubContext.Clients
                .User(mute.MuterId.ToString())
                .UserMutedStateChanged(mute.SubjectId, false);
            });
        }
        
        public async Task NotifyClients(DomainEvent domain)
        {
            var entity = JsonSerializer.Deserialize(domain.Entity, Type.GetType(domain.Type), serializerOptions);
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