using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountSettingsConfiguration : IEntityTypeConfiguration<AccountSettings>
    {
        public void Configure(EntityTypeBuilder<AccountSettings> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(b => b.AccessibilitySettings).WithOne(b => b.AccountSettings).HasForeignKey<AccessibilitySettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.AppearanceSettings).WithOne(b => b.AccountSettings).HasForeignKey<AppearanceSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.AdvancedSettings).WithOne(b => b.AccountSettings).HasForeignKey<AdvancedSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.BillingInformation).WithOne(b => b.AccountSettings).HasForeignKey<BillingInformation>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.ChatSettings).WithOne(b => b.AccountSettings).HasForeignKey<ChatSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.FriendRequestSettings).WithOne(b => b.AccountSettings).HasForeignKey<FriendRequestSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.KeybindSettings).WithOne(b => b.AccountSettings).HasForeignKey<KeybindSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Language).WithMany(b => b.AccountSettings).HasForeignKey(b => b.LanguageId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.NotificationSettings).WithOne(b => b.AccountSettings).HasForeignKey<NotificationSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.PrivacySettings).WithOne(b => b.AccountSettings).HasForeignKey<PrivacySettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);

            // Vi skal har lave ChatSettings med tænker det kommer til at se sådan ud \\
            /* builder.HasOne(b=>b.ChatSettings).WithOne(b => b.AccountSettings).HasForeignKey<ChatSettings>(b => b.AccountSettingsId).OnDelete(DeleteBehavior.Cascade);
             * builder.HasOne(b => b.KeybindSettings).WithOne(b => b.AccountSettings).HasForeignKey<KeybindSettings>(b => b.AccountSettingsId).OnDelete(DeleteBehavior.Cascade);*/

            builder.HasOne(b => b.SoundboardSettings).WithOne(b => b.AccountSettings).HasForeignKey<SoundboardSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.StreamerModeSettings).WithOne(b => b.AccountSettings).HasForeignKey<StreamerModeSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.VideoSettings).WithOne(b => b.AccountSettings).HasForeignKey<VideoSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.VoiceSettings).WithOne(b => b.AccountSettings).HasForeignKey<VoiceSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.ActivitySettings).WithOne(b => b.AccountSettings).HasForeignKey<ActivitySettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
//AccountSoundboardMuteConfiguration