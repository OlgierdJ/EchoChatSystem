using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface INotificationSettings
{
    bool AllowActivityEndNotificationSound { get; set; }
    bool AllowActivityStartNotificationSound { get; set; }
    bool AllowActivityUserJoinNotificationSound { get; set; }
    bool AllowActivityUserLeaveNotificationSound { get; set; }
    bool AllowDeafenNotificationSound { get; set; }
    bool AllowIncomingRingNotificationSound { get; set; }
    bool AllowInvitedToSpeakNotificationSound { get; set; }
    bool AllowMessageNotificationSound { get; set; }
    bool AllowMuteNotificationSound { get; set; }
    bool AllowOutgoingRingNotificationSound { get; set; }
    bool AllowPTTActivateNotificationSound { get; set; }
    bool AllowPTTDeactivateNotificationSound { get; set; }
    bool AllowStreamStartedNotificationSound { get; set; }
    bool AllowStreamStoppedNotificationSound { get; set; }
    bool AllowUndeafenNotificationSound { get; set; }
    bool AllowUnmuteNotificationSound { get; set; }
    bool AllowUserJoinNotificationSound { get; set; }
    bool AllowUserLeaveNotificationSound { get; set; }
    bool AllowUserMovedNotificationSound { get; set; }
    bool AllowViewerJoinNotificationSound { get; set; }
    bool AllowViewerLeaveNotificationSound { get; set; }
    bool AllowVoiceDisconnectedNotificationSound { get; set; }
    bool DisableAllNotificationSounds { get; set; }
    bool EnableDesktopNotifications { get; set; }
    bool EnableSameChannelNotifications { get; set; }
    bool EnableTaskbarFlashing { get; set; }
    bool EnableUnreadMessageBadge { get; set; }
    bool FocusModeEnabled { get; set; }
    ulong Id { get; set; }
    byte PushNotificationInactiveTimeoutInMinutes { get; set; }
    bool ReceiveAnnouncementAndUpdateEmails { get; set; }
    bool ReceiveCommunicationEmails { get; set; }
    bool ReceiveRecommendationEmails { get; set; }
    bool ReceiveSocialEmails { get; set; }
    bool ReceiveTipEmails { get; set; }
    TextToSpeechNotificationMode TextToSpeechNotificationMode { get; set; }
}

public class NotificationSettingsDTO : INotificationSettings
{
    //her lave det om til at NotificationSettings kigger på AccountSettings i stedefor a kigge Account
    public ulong Id { get; set; }
    public bool EnableDesktopNotifications { get; set; }
    public bool EnableUnreadMessageBadge { get; set; }
    public bool EnableTaskbarFlashing { get; set; }
    public byte PushNotificationInactiveTimeoutInMinutes { get; set; } //min 1, max 10

    /*
     * Implement community activity alerts or something like that later maybe
     */

    public TextToSpeechNotificationMode TextToSpeechNotificationMode { get; set; }
    public bool FocusModeEnabled { get; set; } //for not receiving in app sounds
    public bool EnableSameChannelNotifications { get; set; }
    public bool DisableAllNotificationSounds { get; set; }
    public bool AllowMessageNotificationSound { get; set; }
    public bool AllowDeafenNotificationSound { get; set; }
    public bool AllowUndeafenNotificationSound { get; set; }
    public bool AllowMuteNotificationSound { get; set; }
    public bool AllowUnmuteNotificationSound { get; set; }
    public bool AllowVoiceDisconnectedNotificationSound { get; set; }
    public bool AllowPTTActivateNotificationSound { get; set; }
    public bool AllowPTTDeactivateNotificationSound { get; set; }
    public bool AllowUserJoinNotificationSound { get; set; }
    public bool AllowUserLeaveNotificationSound { get; set; }
    public bool AllowUserMovedNotificationSound { get; set; }
    public bool AllowOutgoingRingNotificationSound { get; set; }
    public bool AllowIncomingRingNotificationSound { get; set; }
    public bool AllowStreamStartedNotificationSound { get; set; }
    public bool AllowStreamStoppedNotificationSound { get; set; }
    public bool AllowViewerJoinNotificationSound { get; set; }
    public bool AllowViewerLeaveNotificationSound { get; set; }
    public bool AllowActivityStartNotificationSound { get; set; }
    public bool AllowActivityEndNotificationSound { get; set; }
    public bool AllowActivityUserJoinNotificationSound { get; set; }
    public bool AllowActivityUserLeaveNotificationSound { get; set; }
    public bool AllowInvitedToSpeakNotificationSound { get; set; }

    public bool ReceiveCommunicationEmails { get; set; }
    public bool ReceiveSocialEmails { get; set; }
    public bool ReceiveAnnouncementAndUpdateEmails { get; set; }
    public bool ReceiveTipEmails { get; set; }
    public bool ReceiveRecommendationEmails { get; set; }
}
