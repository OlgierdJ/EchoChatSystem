using Echo.Application.Clients.EchoChatPushNotificationServiceClients;
using Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;
using Echo.Application.Contracts.DTO.EchoCore.UserCore;
using Echo.Application.Contracts.DTO.EchoCore.UserCore.SettingsCore;
using Echo.Application.Contracts.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;

namespace Echo.Chat.Web.Client.Provider;

public interface IUserContainer
{
    UserFullDTO self { get; set; }

    Task ConnectAsync(string token);
    ValueTask DisposeAsync();
    Task InitializeAsync();

    event Action SessionChangeOccured;
}

public class UserContainer : IAsyncDisposable, IUserContainer
{

    public event Action SessionChangeOccured;
    public UserFullDTO self { get; set; }

    private readonly EchoChatPushNotificationServiceClient signalRClient;
    private readonly ILocalStorageService localStorage;
    private readonly NavigationManager nav;

    public UserContainer(EchoChatPushNotificationServiceClient signalRClient, ILocalStorageService localStorage, NavigationManager nav)
    {
        this.signalRClient = signalRClient;
        this.localStorage = localStorage;
        this.nav = nav;
    }

    public async Task ConnectAsync(string token)
    {
        var connection = signalRClient.Connection;
        if (connection == null || connection.State == HubConnectionState.Disconnected)
        {
            await signalRClient.Connect(token);
        }
    }

    public async Task InitializeAsync()
    {
        #region sub to event
        signalRClient.ConnectionOpened += SignalRClient_ConnectionOpened; ;
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
        signalRClient.ChatMessagePinAdded += SignalRClient_ChatMessagePinAdded; ;
        signalRClient.ChatMessagePinRemoved += SignalRClient_ChatMessagePinRemoved; ;
        #endregion

        var Token = await localStorage.GetItemAsStringAsync("Token");

        if (Token.IsNullOrEmpty())
        {
            return;
        }

        await ConnectAsync(Token);
    }

    private void SignalRClient_ChatMessagePinRemoved(ulong chatId, ulong messageId)
    {
        var ChatMessageUpdated = self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Messages.FirstOrDefault(e => e.Id == messageId);
        ChatMessageUpdated.IsPinned = false;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMessagePinAdded(ulong chatId, ulong messageId)
    {
        var ChatMessageUpdated = self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Messages.FirstOrDefault(e => e.Id == messageId);
        ChatMessageUpdated.IsPinned = true;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ConnectionOpened()
    {

        Console.WriteLine("Connected to client.");
    }

    private void SignalRClient_VoiceSettingsUpdated(VoiceSettingsDTO voiceSettingsDTO)
    {
        self.VoiceSettings = voiceSettingsDTO;
        SessionChangeOccured?.Invoke();
    }



    private void SignalRClient_UserProfileChanged(ulong accountId, string displayName, string avatarFileURL, string? about, string bannerColor)
    {
        self.UserProfile.User.ImageIconURL = avatarFileURL;
        self.UserProfile.User.Name = displayName;
        self.UserProfile.AboutMe = about;
        self.UserProfile.BannerColour = bannerColor;

        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_UserMutedStateChanged(ulong chatId, bool hidden)
    {
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Hidden = hidden;

        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_UserActivityStatusChanged(ulong accountId, byte activityStatusId, string? customMessage)
    {
        self.ActiveStatus.DisplayedContent = customMessage;
        self.ActiveStatus.Id = activityStatusId;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_SensitiveDataUpdated(string email, string? phoneNumber)
    {
        self.Email = email;
        self.PhoneNumber = phoneNumber;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_LeftChat(ulong chatId)
    {
        var LeftChat = self.DirectMessages.FirstOrDefault(e => e.Id == chatId);
        self.DirectMessages.Remove(LeftChat);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_FriendRequestRemoved(RequestType type, ulong requestId)
    {
        Console.WriteLine(self.Requests.Count);
        var FriendRequestRemoved = self.Requests.FirstOrDefault(e => e.Id == requestId && e.Type == type);
        self.Requests?.Remove(FriendRequestRemoved);
        Console.WriteLine(self.Requests.Count);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_FriendRequestAdded(FriendRequestDTO friendRequestDTO)
    {
        self.Requests?.Add(friendRequestDTO);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_FriendRemoved(ulong accountId)
    {
        var FriendRemoved = self.Friends.FirstOrDefault(e => e.Id == accountId);
        self.Friends?.Remove(FriendRemoved);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_FriendAdded(UserDTO userDTO)
    {
        self.Friends.Add(userDTO);
        SessionChangeOccured?.Invoke();
    }

    private async void SignalRClient_ForceLogoutAsync()
    {
        await localStorage.ClearAsync();
        nav.NavigateTo("https://localhost:7283/login");
    }

    private void SignalRClient_ExternalUserVolumeChanged(ulong accountId, byte volume)
    {
        throw new NotImplementedException();
    }

    private void SignalRClient_ExternalUserNoteChanged(ulong accountId, string note)
    {
        self.DirectMessages.Select(e =>
        {
            return e.Participants.FirstOrDefault(p => p.Id == accountId);
        }).Select(e => e.Profile.Note = note);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ExternalUserNameChanged(ulong accountId, string name)
    {
        var friend = self.Friends.FirstOrDefault(e => e.Id == accountId);
        if (friend is not null)
        {
            friend.DisplayName = name;
        }
        self.DirectMessages.Select(e =>
        {
            return e.Participants.FirstOrDefault(p => p.Id == accountId);
        }).Select(e => e.Profile.User.DisplayName = name);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMemberOwnershipChanged(ulong chatId, ulong accountId, bool isOwner)
    {
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Participants.FirstOrDefault(e => e.Id == accountId).IsOwner = isOwner;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMemberLeft(ulong chatId, ulong accountId)
    {
        var chat = self.DirectMessages.FirstOrDefault(e => e.Id == chatId);
        var removemember = chat.Participants.FirstOrDefault(e => e.Id == accountId);
        chat.Participants.Remove(removemember);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMemberJoined(ulong chatId, MemberDTO memberDTO)
    {
        var chat = self.DirectMessages.FirstOrDefault(e => e.Id == chatId);
        if (!chat.Participants.Select(e => e.Id).Contains(memberDTO.Id))
        {
            chat.Participants.Add(memberDTO);
        }
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatLastReadMessageChanged(ulong chatId, ulong? messageId)
    {
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId).LastReadMessageId = messageId;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatJoined(ChatDTO chatDTO)
    {
        self.DirectMessages.Add(chatDTO);
        Console.WriteLine(chatDTO.Name);
        Console.WriteLine(chatDTO.Id);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatHiddenStateChanged(ulong chatId, bool hidden)
    {
        Console.WriteLine(nameof(SignalRClient_ChatHiddenStateChanged) + chatId);
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Hidden = hidden;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_BlockedUserRemoved(ulong accountId)
    {
        var blockedremove = self.BlockedUsers.FirstOrDefault(e => e.Id == accountId);
        self.BlockedUsers.Remove(blockedremove);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_BlockedUserAdded(UserMinimalDTO userMinimal)
    {
        self.BlockedUsers.Add(userMinimal);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatUpdated(ulong chatId, string chatName, string? iconUrl)
    {
        var ChatUpdated = self.DirectMessages.FirstOrDefault(e => e.Id == chatId);
        ChatUpdated.Name = chatName;
        ChatUpdated.IconUrl = iconUrl;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMessageUpdated(ulong chatId, ulong messageId, string content, DateTime? timeEdited)
    {
        var ChatMessageUpdated = self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Messages.FirstOrDefault(e => e.Id == messageId);
        ChatMessageUpdated.Content = content;
        ChatMessageUpdated.TimeEdited = timeEdited;
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMessageRemoved(ulong chatId, ulong messageId)
    {
        var ChatMessageUpdated = self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Messages.FirstOrDefault(e => e.Id == messageId);
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Messages.Remove(ChatMessageUpdated);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMessageAdded(ulong chatId, MessageDTO messageDTO)
    {
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId)?.Messages?.Add(messageDTO);
        Console.WriteLine(chatId + "" + messageDTO.Id + "" + DateTime.UtcNow);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_ChatMutedStateChanged(ulong chatId, bool state)
    {
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId).Muted = state;
        SessionChangeOccured?.Invoke();
    }

    //vi skal jo ikke noget med den det mere backend som skal bruge det eller nå man sender en besker
    private void SignalRClient_ChatInviteRemoved(ulong chatId, string invitecode)
    {
        throw new NotImplementedException();
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId);
        SessionChangeOccured?.Invoke();
    }
    //vi skal jo ikke noget med den det mere backend som skal bruge det eller nå man sender en besker
    private void SignalRClient_ChatInviteAdded(ulong chatId, InviteMinimalDTO inviteMinimal)
    {
        throw new NotImplementedException();
        self.DirectMessages.FirstOrDefault(e => e.Id == chatId);
        SessionChangeOccured?.Invoke();
    }

    private void SignalRClient_UserUnmuted(ulong accountId)
    {
        throw new NotImplementedException();
    }

    public async ValueTask DisposeAsync()
    {
        await signalRClient.Disconnect();
    }
}
