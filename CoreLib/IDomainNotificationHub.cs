using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.FriendCore;
using CoreLib.Entities.EchoCore.ReportCore.Bug;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Feedback;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public interface IDomainNotificationHub
    {
        //Task Login(string user);
        Task JoinGroup(string groupName);
        Task JoinGroups(string[] groupNames);
        Task LeaveGroup(string groupName);
        Task LeaveGroups(string[] groupNames);
        Task OnConnectedAsync();
        Task OnDisconnectedAsync();

        Task ReceiveAccountCreateMessage(Account entity);
        Task ReceiveAccountUpdateMessage(Account entity);
        Task ReceiveAccountDeleteMessage(Account entity);
        Task ReceiveAccountActivityStatusCreateMessage(AccountActivityStatus entity);
        Task ReceiveAccountActivityStatusUpdateMessage(AccountActivityStatus entity);
        Task ReceiveAccountActivityStatusDeleteMessage(AccountActivityStatus entity);
        Task ReceiveAccountBlockCreateMessage(AccountBlock entity);
        Task ReceiveAccountBlockUpdateMessage(AccountBlock entity);
        Task ReceiveAccountBlockDeleteMessage(AccountBlock entity);
        Task ReceiveAccountConnectionCreateMessage(AccountConnection entity);
        Task ReceiveAccountConnectionUpdateMessage(AccountConnection entity);
        Task ReceiveAccountConnectionDeleteMessage(AccountConnection entity);
        Task ReceiveAccountCustomStatusCreateMessage(AccountCustomStatus entity);
        Task ReceiveAccountCustomStatusUpdateMessage(AccountCustomStatus entity);
        Task ReceiveAccountCustomStatusDeleteMessage(AccountCustomStatus entity);
        Task ReceiveAccountMuteCreateMessage(AccountMute entity);
        Task ReceiveAccountMuteUpdateMessage(AccountMute entity);
        Task ReceiveAccountMuteDeleteMessage(AccountMute entity);
        Task ReceiveAccountNicknameCreateMessage(AccountNickname entity);
        Task ReceiveAccountNicknameUpdateMessage(AccountNickname entity);
        Task ReceiveAccountNicknameDeleteMessage(AccountNickname entity);
        Task ReceiveAccountNoteCreateMessage(AccountNote entity);
        Task ReceiveAccountNoteUpdateMessage(AccountNote entity);
        Task ReceiveAccountNoteDeleteMessage(AccountNote entity);
        Task ReceiveAccountProfileCreateMessage(AccountProfile entity);
        Task ReceiveAccountProfileUpdateMessage(AccountProfile entity);
        Task ReceiveAccountProfileDeleteMessage(AccountProfile entity);
        Task ReceiveAccountRoleCreateMessage(AccountRole entity);
        Task ReceiveAccountRoleUpdateMessage(AccountRole entity);
        Task ReceiveAccountRoleDeleteMessage(AccountRole entity);
        Task ReceiveAccountSessionCreateMessage(AccountSession entity);
        Task ReceiveAccountSessionUpdateMessage(AccountSession entity);
        Task ReceiveAccountSessionDeleteMessage(AccountSession entity);
        Task ReceiveAccountSettingsCreateMessage(AccountSettings entity);
        Task ReceiveAccountSettingsUpdateMessage(AccountSettings entity);
        Task ReceiveAccountSettingsDeleteMessage(AccountSettings entity);
        Task ReceiveAccountSoundboardMuteCreateMessage(AccountSoundboardMute entity);
        Task ReceiveAccountSoundboardMuteUpdateMessage(AccountSoundboardMute entity);
        Task ReceiveAccountSoundboardMuteDeleteMessage(AccountSoundboardMute entity);
        Task ReceiveAccountVideoMuteCreateMessage(AccountVideoMute entity);
        Task ReceiveAccountVideoMuteUpdateMessage(AccountVideoMute entity);
        Task ReceiveAccountVideoMuteDeleteMessage(AccountVideoMute entity);
        Task ReceiveAccountViolationCreateMessage(AccountViolation entity);
        Task ReceiveAccountViolationUpdateMessage(AccountViolation entity);
        Task ReceiveAccountViolationDeleteMessage(AccountViolation entity);
        Task ReceiveAccountViolationAppealCreateMessage(AccountViolationAppeal entity);
        Task ReceiveAccountViolationAppealUpdateMessage(AccountViolationAppeal entity);
        Task ReceiveAccountViolationAppealDeleteMessage(AccountViolationAppeal entity);
        Task ReceiveAccountViolationAppealReviewCreateMessage(AccountViolationAppealReview entity);
        Task ReceiveAccountViolationAppealReviewUpdateMessage(AccountViolationAppealReview entity);
        Task ReceiveAccountViolationAppealReviewDeleteMessage(AccountViolationAppealReview entity);

        Task ReceiveAccessibilitySettingsMessageCreateMessage(AccessibilitySettings entity);
        Task ReceiveAccessibilitySettingsMessageUpdateMessage(AccessibilitySettings entity);
        Task ReceiveAccessibilitySettingsMessageDeleteMessage(AccessibilitySettings entity);

        Task ReceiveActivitySettingsMessageCreateMessage(ActivitySettings entity);
        Task ReceiveActivitySettingsMessageUpdateMessage(ActivitySettings entity);
        Task ReceiveActivitySettingsMessageDeleteMessage(ActivitySettings entity);

        Task ReceiveAdvancedSettingsMessageCreateMessage(AdvancedSettings entity);
        Task ReceiveAdvancedSettingsMessageUpdateMessage(AdvancedSettings entity);
        Task ReceiveAdvancedSettingsMessageDeleteMessage(AdvancedSettings entity);

        Task ReceiveAppearanceSettingsMessageCreateMessage(AppearanceSettings entity);
        Task ReceiveAppearanceSettingsMessageUpdateMessage(AppearanceSettings entity);
        Task ReceiveAppearanceSettingsMessageDeleteMessage(AppearanceSettings entity);

        Task ReceiveBillingInformationMessageCreateMessage(BillingInformation entity);
        Task ReceiveBillingInformationMessageUpdateMessage(BillingInformation entity);
        Task ReceiveBillingInformationMessageDeleteMessage(BillingInformation entity);

        Task ReceiveChatSettingsMessageCreateMessage(ChatSettings entity);
        Task ReceiveChatSettingsMessageUpdateMessage(ChatSettings entity);
        Task ReceiveChatSettingsMessageDeleteMessage(ChatSettings entity);

        Task ReceiveFriendRequestSettingsMessageCreateMessage(FriendRequestSettings entity);
        Task ReceiveFriendRequestSettingsMessageUpdateMessage(FriendRequestSettings entity);
        Task ReceiveFriendRequestSettingsMessageDeleteMessage(FriendRequestSettings entity);

        Task ReceiveGameOverlaySettingsMessageCreateMessage(GameOverlaySettings entity);
        Task ReceiveGameOverlaySettingsMessageUpdateMessage(GameOverlaySettings entity);
        Task ReceiveGameOverlaySettingsMessageDeleteMessage(GameOverlaySettings entity);

        Task ReceiveKeybindSettingsMessageCreateMessage(KeybindSettings entity);
        Task ReceiveKeybindSettingsMessageUpdateMessage(KeybindSettings entity);
        Task ReceiveKeybindSettingsMessageDeleteMessage(KeybindSettings entity);

        Task ReceiveLanguageMessageCreateMessage(Language entity);
        Task ReceiveLanguageMessageUpdateMessage(Language entity);
        Task ReceiveLanguageMessageDeleteMessage(Language entity);

        Task ReceiveNotificationSettingsMessageCreateMessage(NotificationSettings entity);
        Task ReceiveNotificationSettingsMessageUpdateMessage(NotificationSettings entity);
        Task ReceiveNotificationSettingsMessageDeleteMessage(NotificationSettings entity);

        Task ReceivePermissionMessageCreateMessage(Permission entity);
        Task ReceivePermissionMessageUpdateMessage(Permission entity);
        Task ReceivePermissionMessageDeleteMessage(Permission entity);

        Task ReceivePrivacySettingsMessageCreateMessage(PrivacySettings entity);
        Task ReceivePrivacySettingsMessageUpdateMessage(PrivacySettings entity);
        Task ReceivePrivacySettingsMessageDeleteMessage(PrivacySettings entity);

        Task ReceiveRoleMessageCreateMessage(Role entity);
        Task ReceiveRoleMessageUpdateMessage(Role entity);
        Task ReceiveRoleMessageDeleteMessage(Role entity);

        Task ReceiveRolePermissionMessageCreateMessage(RolePermission entity);
        Task ReceiveRolePermissionMessageUpdateMessage(RolePermission entity);
        Task ReceiveRolePermissionMessageDeleteMessage(RolePermission entity);

        Task ReceiveSoundboardSettingsMessageCreateMessage(SoundboardSettings entity);
        Task ReceiveSoundboardSettingsMessageUpdateMessage(SoundboardSettings entity);
        Task ReceiveSoundboardSettingsMessageDeleteMessage(SoundboardSettings entity);

        Task ReceiveStreamerModeSettingsMessageCreateMessage(StreamerModeSettings entity);
        Task ReceiveStreamerModeSettingsMessageUpdateMessage(StreamerModeSettings entity);
        Task ReceiveStreamerModeSettingsMessageDeleteMessage(StreamerModeSettings entity);

        Task ReceiveThemeMessageCreateMessage(Theme entity);
        Task ReceiveThemeMessageUpdateMessage(Theme entity);
        Task ReceiveThemeMessageDeleteMessage(Theme entity);

        Task ReceiveVideoSettingsMessageCreateMessage(VideoSettings entity);
        Task ReceiveVideoSettingsMessageUpdateMessage(VideoSettings entity);
        Task ReceiveVideoSettingsMessageDeleteMessage(VideoSettings entity);

        Task ReceiveVoiceSettingsMessageCreateMessage(VoiceSettings entity);
        Task ReceiveVoiceSettingsMessageUpdateMessage(VoiceSettings entity);
        Task ReceiveVoiceSettingsMessageDeleteMessage(VoiceSettings entity);

        Task ReceiveWindowSettingsMessageCreateMessage(WindowSettings entity);
        Task ReceiveWindowSettingsMessageUpdateMessage(WindowSettings entity);
        Task ReceiveWindowSettingsMessageDeleteMessage(WindowSettings entity);

        Task ReceiveChatMessageCreateMessage(Chat entity);
        Task ReceiveChatMessageUpdateMessage(Chat entity);
        Task ReceiveChatMessageDeleteMessage(Chat entity);

        Task ReceiveChatAccountMessageTrackerMessageCreateMessage(ChatAccountMessageTracker entity);
        Task ReceiveChatAccountMessageTrackerMessageUpdateMessage(ChatAccountMessageTracker entity);
        Task ReceiveChatAccountMessageTrackerMessageDeleteMessage(ChatAccountMessageTracker entity);

        Task ReceiveChatInviteMessageCreateMessage(ChatInvite entity);
        Task ReceiveChatInviteMessageUpdateMessage(ChatInvite entity);
        Task ReceiveChatInviteMessageDeleteMessage(ChatInvite entity);

        Task ReceiveChatMessageMessageCreateMessage(ChatMessage entity);
        Task ReceiveChatMessageMessageUpdateMessage(ChatMessage entity);
        Task ReceiveChatMessageMessageDeleteMessage(ChatMessage entity);

        Task ReceiveChatMessageAttachmentMessageCreateMessage(ChatMessageAttachment entity);
        Task ReceiveChatMessageAttachmentMessageUpdateMessage(ChatMessageAttachment entity);
        Task ReceiveChatMessageAttachmentMessageDeleteMessage(ChatMessageAttachment entity);

        Task ReceiveChatMessagePinMessageCreateMessage(ChatMessagePin entity);
        Task ReceiveChatMessagePinMessageUpdateMessage(ChatMessagePin entity);
        Task ReceiveChatMessagePinMessageDeleteMessage(ChatMessagePin entity);

        Task ReceiveChatMuteMessageCreateMessage(ChatMute entity);
        Task ReceiveChatMuteMessageUpdateMessage(ChatMute entity);
        Task ReceiveChatMuteMessageDeleteMessage(ChatMute entity);

        Task ReceiveChatParticipancyMessageCreateMessage(ChatParticipancy entity);
        Task ReceiveChatParticipancyMessageUpdateMessage(ChatParticipancy entity);
        Task ReceiveChatParticipancyMessageDeleteMessage(ChatParticipancy entity);

        Task ReceiveChatPinboardMessageCreateMessage(ChatPinboard entity);
        Task ReceiveChatPinboardMessageUpdateMessage(ChatPinboard entity);
        Task ReceiveChatPinboardMessageDeleteMessage(ChatPinboard entity);

        Task ReceiveFriendshipMessageCreateMessage(Friendship entity);
        Task ReceiveFriendshipMessageUpdateMessage(Friendship entity);
        Task ReceiveFriendshipMessageDeleteMessage(Friendship entity);

        Task ReceiveFriendshipParticipancyMessageCreateMessage(FriendshipParticipancy entity);
        Task ReceiveFriendshipParticipancyMessageUpdateMessage(FriendshipParticipancy entity);
        Task ReceiveFriendshipParticipancyMessageDeleteMessage(FriendshipParticipancy entity);

        Task ReceiveFriendSuggestionMessageCreateMessage(FriendSuggestion entity);
        Task ReceiveFriendSuggestionMessageUpdateMessage(FriendSuggestion entity);
        Task ReceiveFriendSuggestionMessageDeleteMessage(FriendSuggestion entity);

        Task ReceiveIncomingFriendRequestMessageCreateMessage(IncomingFriendRequest entity);
        Task ReceiveIncomingFriendRequestMessageUpdateMessage(IncomingFriendRequest entity);
        Task ReceiveIncomingFriendRequestMessageDeleteMessage(IncomingFriendRequest entity);

        Task ReceiveOutgoingFriendRequestMessageCreateMessage(OutgoingFriendRequest entity);
        Task ReceiveOutgoingFriendRequestMessageUpdateMessage(OutgoingFriendRequest entity);
        Task ReceiveOutgoingFriendRequestMessageDeleteMessage(OutgoingFriendRequest entity);

        Task ReceiveBugReportMessageCreateMessage(BugReport entity);
        Task ReceiveBugReportMessageUpdateMessage(BugReport entity);
        Task ReceiveBugReportMessageDeleteMessage(BugReport entity);

        Task ReceiveBugReportReasonMessageCreateMessage(BugReportReason entity);
        Task ReceiveBugReportReasonMessageUpdateMessage(BugReportReason entity);
        Task ReceiveBugReportReasonMessageDeleteMessage(BugReportReason entity);

        Task ReceiveCustomStatusReportMessageCreateMessage(CustomStatusReport entity);
        Task ReceiveCustomStatusReportMessageUpdateMessage(CustomStatusReport entity);
        Task ReceiveCustomStatusReportMessageDeleteMessage(CustomStatusReport entity);

        Task ReceiveCustomStatusReportReasonMessageCreateMessage(CustomStatusReportReason entity);
        Task ReceiveCustomStatusReportReasonMessageUpdateMessage(CustomStatusReportReason entity);
        Task ReceiveCustomStatusReportReasonMessageDeleteMessage(CustomStatusReportReason entity);

        Task ReceiveReportedCustomStatusMessageCreateMessage(ReportedCustomStatus entity);
        Task ReceiveReportedCustomStatusMessageUpdateMessage(ReportedCustomStatus entity);
        Task ReceiveReportedCustomStatusMessageDeleteMessage(ReportedCustomStatus entity);

        Task ReceiveFeedbackReportMessageCreateMessage(FeedbackReport entity);
        Task ReceiveFeedbackReportMessageUpdateMessage(FeedbackReport entity);
        Task ReceiveFeedbackReportMessageDeleteMessage(FeedbackReport entity);

        Task ReceiveFeedbackReportReasonMessageCreateMessage(FeedbackReportReason entity);
        Task ReceiveFeedbackReportReasonMessageUpdateMessage(FeedbackReportReason entity);
        Task ReceiveFeedbackReportReasonMessageDeleteMessage(FeedbackReportReason entity);

        Task ReceiveMessageReportMessageCreateMessage(MessageReport entity);
        Task ReceiveMessageReportMessageUpdateMessage(MessageReport entity);
        Task ReceiveMessageReportMessageDeleteMessage(MessageReport entity);

        Task ReceiveMessageReportReasonMessageCreateMessage(MessageReportReason entity);
        Task ReceiveMessageReportReasonMessageUpdateMessage(MessageReportReason entity);
        Task ReceiveMessageReportReasonMessageDeleteMessage(MessageReportReason entity);

        Task ReceiveReportedMessageMessageCreateMessage(ReportedMessage entity);
        Task ReceiveReportedMessageMessageUpdateMessage(ReportedMessage entity);
        Task ReceiveReportedMessageMessageDeleteMessage(ReportedMessage entity);

        Task ReceiveReportedMessageAttachmentMessageCreateMessage(ReportedMessageAttachment entity);
        Task ReceiveReportedMessageAttachmentMessageUpdateMessage(ReportedMessageAttachment entity);
        Task ReceiveReportedMessageAttachmentMessageDeleteMessage(ReportedMessageAttachment entity);

        Task ReceiveProfileReportMessageCreateMessage(ProfileReport entity);
        Task ReceiveProfileReportMessageUpdateMessage(ProfileReport entity);
        Task ReceiveProfileReportMessageDeleteMessage(ProfileReport entity);

        Task ReceiveProfileReportReasonMessageCreateMessage(ProfileReportReason entity);
        Task ReceiveProfileReportReasonMessageUpdateMessage(ProfileReportReason entity);
        Task ReceiveProfileReportReasonMessageDeleteMessage(ProfileReportReason entity);

        Task ReceiveReportedProfileMessageCreateMessage(ReportedProfile entity);
        Task ReceiveReportedProfileMessageUpdateMessage(ReportedProfile entity);
        Task ReceiveReportedProfileMessageDeleteMessage(ReportedProfile entity);

        Task ReceiveUserMessageCreateMessage(User entity);
        Task ReceiveUserMessageUpdateMessage(User entity);
        Task ReceiveUserMessageDeleteMessage(User entity);

        Task ReceiveSecurityCredentialsMessageCreateMessage(SecurityCredentials entity);
        Task ReceiveSecurityCredentialsMessageUpdateMessage(SecurityCredentials entity);
        Task ReceiveSecurityCredentialsMessageDeleteMessage(SecurityCredentials entity);

        Task ReceiveUpdateMessage(string message);
    }
}
