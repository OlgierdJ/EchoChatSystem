using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.EchoCore.UserCore.SettingsCore;
using CoreLib.Entities.Enums;
using CoreLib.Handlers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;

namespace EchoWebapp.Client.Provider
{
    public interface IUserContainer
    {
        UserFullDTO self { get; set; }

        Task ConnectAsync(string token);
        ValueTask DisposeAsync();
        Task InitializeAsync();
    }

    public class UserContainer : IAsyncDisposable, IUserContainer
    {
        private readonly SignalRClientService signalRClient;
        private readonly ILocalStorageService localStorage;
        private string Token;
        public UserFullDTO self { get; set; }

        public UserContainer(SignalRClientService signalRClient, ILocalStorageService localStorage)
        {
            this.signalRClient = signalRClient;
            this.localStorage = localStorage;
        }

        public async Task ConnectAsync(string token)
        {
            if (signalRClient.Connection.State == HubConnectionState.Disconnected)
            {
                await signalRClient.Connect(token);
            }
        }

        public async Task InitializeAsync()
        {
            #region sus to event
            signalRClient.BlockedUserAdded += SignalRClient_BlockedUserAdded;
            signalRClient.BlockedUserRemoved += SignalRClient_BlockedUserRemoved;
            signalRClient.ChatHiddenStateChanged += SignalRClient_ChatHiddenStateChanged;
            signalRClient.ChatInviteAdded += SignalRClient_ChatInviteAdded;
            signalRClient.ChatInviteRemoved += SignalRClient_ChatInviteRemoved;
            signalRClient.ChatJoined += SignalRClient_ChatJoined;
            signalRClient.ChatLastReadMessageChanged += SignalRClient_ChatLastReadMessageChanged;
            signalRClient.ChatMemberJoined += SignalRClient_ChatMemberJoined;
            signalRClient.ChatMemberLeft += SignalRClient_ChatMemberLeft;
            signalRClient.ChatMemberOwnershipChanged += SignalRClient_ChatMemberOwnershipChanged;
            signalRClient.ChatMessageAdded += SignalRClient_ChatMessageAdded;
            signalRClient.ChatMessageRemoved += SignalRClient_ChatMessageRemoved;
            signalRClient.ChatMutedStateChanged += SignalRClient_ChatMutedStateChanged;
            signalRClient.ChatMessageUpdated += SignalRClient_ChatMessageUpdated;
            signalRClient.ChatUpdated += SignalRClient_ChatUpdated;
            signalRClient.ExternalUserNameChanged += SignalRClient_ExternalUserNameChanged;
            signalRClient.ExternalUserNoteChanged += SignalRClient_ExternalUserNoteChanged;
            signalRClient.ExternalUserVolumeChanged += SignalRClient_ExternalUserVolumeChanged;
            signalRClient.ForceLogout += SignalRClient_ForceLogoutAsync;
            signalRClient.FriendAdded += SignalRClient_FriendAdded;
            signalRClient.FriendRemoved += SignalRClient_FriendRemoved;
            signalRClient.FriendRequestAdded += SignalRClient_FriendRequestAdded;
            signalRClient.FriendRequestRemoved += SignalRClient_FriendRequestRemoved;
            signalRClient.LeftChat += SignalRClient_LeftChat;
            signalRClient.SensitiveDataUpdated += SignalRClient_SensitiveDataUpdated;
            signalRClient.UserActivityStatusChanged += SignalRClient_UserActivityStatusChanged;
            signalRClient.UserMutedStateChanged += SignalRClient_UserMutedStateChanged;
            signalRClient.UserProfileChanged += SignalRClient_UserProfileChanged;
            signalRClient.UserUnmuted += SignalRClient_UserUnmuted;
            signalRClient.VoiceSettingsUpdated += SignalRClient_VoiceSettingsUpdated;
            #endregion

            var Token = await localStorage.GetItemAsStringAsync("Token");

            if (Token.IsNullOrEmpty())
            {
                return;
            }

            await ConnectAsync(Token);
        }

        private void SignalRClient_VoiceSettingsUpdated(VoiceSettingsDTO voiceSettingsDTO)
        {
            self.VoiceSettings = voiceSettingsDTO;
        }

        private void SignalRClient_UserUnmuted(ulong accountId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_UserProfileChanged(ulong accountId, string displayName, string avatarFileURL, string? about, string bannerColor)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_UserMutedStateChanged(ulong chatId, bool hidden)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_UserActivityStatusChanged(ulong accountId, byte activityStatusId, string? customMessage)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_SensitiveDataUpdated(string email, string? phoneNumber)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_LeftChat(ulong chatId)
        {
            self.DirectMessages.Remove(self.DirectMessages.FirstOrDefault(e => e.Id == chatId));
        }

        private void SignalRClient_FriendRequestRemoved(RequestType type, ulong requestId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_FriendRequestAdded(FriendRequestDTO friendRequestDTO)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_FriendRemoved(ulong accountId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_FriendAdded(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        private async void SignalRClient_ForceLogoutAsync()
        {
            await localStorage.ClearAsync();
        }

        private void SignalRClient_ExternalUserVolumeChanged(ulong accountId, byte volume)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ExternalUserNoteChanged(ulong accountId, string note)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ExternalUserNameChanged(ulong accountId, string name)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMemberOwnershipChanged(ulong chatId, bool isOwner)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMemberLeft(ulong chatId, ulong accountId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMemberJoined(ulong chatId, MemberDTO memberDTO)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatLastReadMessageChanged(ulong chatId, ulong? messageId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatJoined(ChatDTO chatDTO)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatInviteRemoved(ulong chatId, string invitecode)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatInviteAdded(ulong chatId, InviteMinimalDTO inviteMinimal)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatHiddenStateChanged(ulong chatId, bool hidden)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_BlockedUserRemoved(ulong accountId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_BlockedUserAdded(UserMinimalDTO userMinimal)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatUpdated(ulong chatId, string chatName, string? iconUrl)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMessageUpdated(ulong chatId, ulong messageId, string content, DateTime? timeEdited)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMessageRemoved(ulong chatId, ulong messageId)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMessageAdded(ulong chatId, MessageDTO messageDTO)
        {
            throw new NotImplementedException();
        }

        private void SignalRClient_ChatMutedStateChanged(ulong chatId, bool state)
        {
            throw new NotImplementedException();
        }

        public async ValueTask DisposeAsync()
        {
            await signalRClient.Disconnect();
        }
    }
}
