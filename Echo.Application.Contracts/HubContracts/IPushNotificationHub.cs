using Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;
using Echo.Application.Contracts.DTO.EchoCore.UserCore;
using Echo.Application.Contracts.DTO.EchoCore.UserCore.SettingsCore;
using Echo.Application.Contracts.Enums;

namespace Echo.Application.Contracts.ApiClients.EchoPushNotification;


public interface IPushNotificationHub
{
    //specific user
    Task BlockedUserAdded(UserMinimalDTO userMinimalDTO);
    Task BlockedUserRemoved(ulong accountId);
    Task ChatHiddenStateChanged(ulong chatId, bool hidden);
    Task FriendAdded(UserDTO userDTO);
    Task FriendRemoved(ulong accountId);
    Task FriendRequestAdded(FriendRequestDTO friendRequestDTO);
    Task FriendRequestRemoved(RequestType type, ulong requestId);
    Task ChatLastReadMessageChanged(ulong chatId, ulong? messageId);
    Task ChatJoined(ChatDTO chatDTO);
    Task LeftChat(ulong chatId);
    Task ChatMutedStateChanged(ulong chatId, bool state);
    Task SensitiveDataUpdated(string email, string? phoneNumber);
    Task VoiceSettingsUpdated(VoiceSettingsDTO voiceSettingsDTO);
    Task ForceLogout();

    //specific chat members
    Task ChatInviteAdded(ulong chatId, InviteMinimalDTO inviteDTO);
    Task ChatInviteRemoved(ulong chatId, string inviteCode);
    Task ChatMemberJoined(ulong chatId, MemberDTO memberDTO);
    Task ChatMemberLeft(ulong chatId, ulong accountId);
    Task ChatMemberOwnershipChanged(ulong chatId, ulong accountId, bool isOwner);
    Task ChatMessageAdded(ulong chatId, MessageDTO messageDTO);
    Task ChatMessageRemoved(ulong chatId, ulong messageId);
    Task ChatMessageUpdated(ulong chatId, ulong messageId, string content, DateTime? timeEdited);
    Task ChatUpdated(ulong chatId, string name, string? iconUrl);
    Task ChatMessagePinAdded(ulong pinboardId, ulong messageId);
    Task ChatMessagePinRemoved(ulong pinboardId, ulong messageId);

    //global events kinda hits lots of different areas and users
    Task ExternalUserNameChanged(ulong accountId, string name);
    Task ExternalUserNoteChanged(ulong accountId, string note);
    Task ExternalUserVolumeChanged(ulong accountId, byte volume);
    Task UserActivityStatusChanged(ulong accountId, byte activityStatusId, string? customMessage);
    Task UserMutedStateChanged(ulong accountId, bool state);
    Task UserProfileChanged(ulong accountId, string displayName, string avatarFileURL, string? about, string bannerColor);
    Task UserUnmuted(ulong accountId);
}
