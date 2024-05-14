using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore
{
    public class NotificationSettingsConfiguration : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff
            builder.Property(b => b.EnableDesktopNotifications).IsRequired();
            builder.Property(b => b.EnableUnreadMessageBadge).IsRequired();
            builder.Property(b => b.EnableTaskbarFlashing).IsRequired();
            builder.Property(b => b.PushNotificationInactiveTimeoutInMinutes).IsRequired();

            builder.Property(b => b.TextToSpeechNotificationMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.FocusModeEnabled).IsRequired();
            builder.Property(b => b.EnableSameChannelNotifications).IsRequired();
            builder.Property(b => b.DisableAllNotificationSounds).IsRequired();
            builder.Property(b => b.AllowMessageNotificationSound).IsRequired();
            builder.Property(b => b.AllowDeafenNotificationSound).IsRequired();
            builder.Property(b => b.AllowMuteNotificationSound).IsRequired();
            builder.Property(b => b.AllowUnmuteNotificationSound).IsRequired();
            builder.Property(b => b.AllowVoiceDisconnectedNotificationSound).IsRequired();
            builder.Property(b => b.AllowPTTActivateNotificationSound).IsRequired();
            builder.Property(b => b.AllowPTTDeactivateNotificationSound).IsRequired();
            builder.Property(b => b.AllowUserJoinNotificationSound).IsRequired();
            builder.Property(b => b.AllowUserLeaveNotificationSound).IsRequired();
            builder.Property(b => b.AllowUserMovedNotificationSound).IsRequired();
            builder.Property(b => b.AllowOutgoingRingNotificationSound).IsRequired();
            builder.Property(b => b.AllowIncomingRingNotificationSound).IsRequired();
            builder.Property(b => b.AllowStreamStartedNotificationSound).IsRequired();
            builder.Property(b => b.AllowStreamStoppedNotificationSound).IsRequired();
            builder.Property(b => b.AllowViewerJoinNotificationSound).IsRequired();
            builder.Property(b => b.AllowViewerLeaveNotificationSound).IsRequired();
            builder.Property(b => b.AllowActivityStartNotificationSound).IsRequired();
            builder.Property(b => b.AllowActivityEndNotificationSound).IsRequired();
            builder.Property(b => b.AllowActivityUserJoinNotificationSound).IsRequired();
            builder.Property(b => b.AllowActivityUserLeaveNotificationSound).IsRequired();
            builder.Property(b => b.AllowInvitedToSpeakNotificationSound).IsRequired();

            builder.Property(b => b.ReceiveCommunicationEmails).IsRequired();
            builder.Property(b => b.ReceiveSocialEmails).IsRequired();
            builder.Property(b => b.ReceiveAnnouncementAndUpdateEmails).IsRequired();
            builder.Property(b => b.ReceiveTipEmails).IsRequired();
            builder.Property(b => b.ReceiveRecommendationEmails).IsRequired();

            builder.HasOne(b => b.AccountSettings).WithOne(e => e.NotificationSettings).HasForeignKey<NotificationSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
