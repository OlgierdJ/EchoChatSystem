using CoreLib.DTO.EchoCore.ChatCore.TextCore;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.EchoCore.UserCore.SettingsCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ReportCore.Bug;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Feedback;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Hubs
{

    public interface IPushNotificationHub
    {
        Task BlockedUserAdded(UserMinimalDTO userMinimalDTO);
        Task BlockedUserRemoved(ulong blockedId);
        Task ChatAdded(ChatDTO chatDTO);
        Task ChatHiddenStateChanged(ulong subjectId, bool hidden);
        Task ChatInviteAdded(ulong subjectId, InviteDTO inviteDTO);
        Task ChatInviteRemoved(ulong subjectId, string inviteCode);
        Task ChatJoined(ChatDTO chatDTO);
        Task ChatMemberJoined(ulong subjectId, MemberDTO memberDTO);
        Task ChatMemberLeft(ulong participantId);
        Task ChatMemberOwnershipChanged(ulong participantId, bool isOwner);
        Task ChatMessageAdded(ulong messageHolderId, MessageDTO messageDTO);
        Task ChatMessageRemoved(ulong messageHolderId, ulong id);
        Task ChatMessageUpdated(ulong messageHolderId, ulong id, string content, DateTime? timeEdited);
        Task ChatMutedStateChanged(ulong subjectId, bool v);
        Task ChatRemoved(ulong id);
        Task ChatUpdated(ulong id, string name, string? iconUrl);
        Task ExternalImageChanged(ulong accountId, string avatarFileURL);
        Task ExternalNameChanged(ulong subjectId, string name);
        Task ExternalProfileChanged(ulong accountId, string displayName, string avatarFileURL, string? about, string bannerColor);
        Task ExternalUserNicknameChanged(ulong subjectId, string nickname);
        Task ExternalUserNoteChanged(ulong subjectId, string note);
        Task ExternalUserVolumeChanged(ulong subjectId, byte volume);
        Task ForceLogout();
        Task FriendAdded(UserDTO userDTO);
        Task FriendRemoved(ulong participantId);
        Task FriendRequestAdded(FriendRequestDTO friendRequestDTO);
        Task FriendRequestRemoved(RequestType outgoing, ulong id);
        Task JoinedChat(ChatDTO chatDTO);
        Task LastReadMessageChanged(ulong coOwnerId, ulong? subjectId);
        Task LeftChat(ulong subjectId);
        Task MemberOwnershipChanged(ulong participantId, bool isOwner);
        Task SensitiveDataUpdated(string email, string? phoneNumber);
        Task UserMuted(ulong subjectId);
        Task UserMutedStateChanged(ulong subjectId, bool v);
        Task UserNoteChanged(ulong subjectId, string note);
        Task UserUnmuted(ulong subjectId);
        Task VoiceSettingsUpdated(VoiceSettingsDTO voiceSettingsDTO);
    }
}
