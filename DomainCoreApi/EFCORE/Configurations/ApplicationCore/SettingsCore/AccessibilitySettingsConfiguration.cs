using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore
{
    public class AccessibilitySettingsConfiguration : IEntityTypeConfiguration<AccessibilitySettings>
    {
        public void Configure(EntityTypeBuilder<AccessibilitySettings> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.SaturationPercent).IsRequired().HasMaxLength(100); // not mapped most of stuff
            builder.Property(b => b.ApplySaturationToCustomColors).IsRequired();
            builder.Property(b => b.AlwaysUnderlineLinks).IsRequired();
            builder.Property(b => b.RoleColorMode).HasConversion<int>().HasDefaultValue(0);

            builder.Property(b => b.SyncProfileThemes).IsRequired();
            builder.Property(b => b.ReducedMotion).IsRequired();
            builder.Property(b => b.AutoPlayGIFsOnAppFocus).IsRequired();
            builder.Property(b => b.PlayAnimatedEmojis).IsRequired();

            builder.Property(b => b.StickerAnimationMode).HasConversion<int>().HasDefaultValue(0);


            builder.Property(b => b.ShowSendMessageButton).IsRequired();
            builder.Property(b => b.UseLegacyChatInput).IsRequired();

            builder.Property(b => b.AllowTextToSpeech).IsRequired();
            builder.Property(b => b.TextToSpeechRate).IsRequired().HasDefaultValue(4).HasMaxLength(40);

            builder.HasOne(b => b.AccountSettings).WithOne(e => e.AccessibilitySettings).HasForeignKey<AccessibilitySettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
