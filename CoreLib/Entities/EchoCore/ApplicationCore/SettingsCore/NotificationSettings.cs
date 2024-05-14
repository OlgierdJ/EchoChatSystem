using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class NotificationSettings : BaseEntity<ulong>
    {
        //her lave det om til at NotificationSettings kigger på AccountSettings i stedefor a kigge Account
        //public ulong AccountSettingsId { get; set; }
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
        public AccountSettings AccountSettings { get; set; }
    }
}
