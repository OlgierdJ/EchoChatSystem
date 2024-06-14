using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class GameOverlaySettingsConfiguration : IEntityTypeConfiguration<GameOverlaySettings>
{
    public void Configure(EntityTypeBuilder<GameOverlaySettings> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();

        builder.Property(b => b.EnableGameOverlay).IsRequired();
        builder.Property(b => b.ToggleOverlayLockKeybind).HasDefaultValue("Shift + L").IsRequired();
        builder.Property(b => b.AvatarSizeMode).HasConversion<int>().IsRequired();
        builder.Property(b => b.DisplayNamesMode).HasConversion<int>().IsRequired();
        builder.Property(b => b.DisplayUsersMode).HasConversion<int>().IsRequired();
        builder.Property(b => b.OverlayNotificationsPlacement).HasConversion<int>().IsRequired();
        builder.Property(b => b.ShowTextChatNotifications).IsRequired();

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.GameOverlaySettings).HasForeignKey<GameOverlaySettings>(b => b.Id);
    }
}
