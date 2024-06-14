using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class ActivitySettingsConfiguration : IEntityTypeConfiguration<ActivitySettings>
{
    public void Configure(EntityTypeBuilder<ActivitySettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff
        builder.Property(b => b.DisplayCurrentActivityAsAStatusMessage).IsRequired();
        builder.Property(b => b.ShareActivityStatusOnLargeServerJoin).IsRequired();
        builder.Property(b => b.AllowFriendsToJoinGame).IsRequired();
        builder.Property(b => b.AllowVoiceChannelParticipantsToJoinGame).IsRequired();

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.ActivitySettings).HasForeignKey<ActivitySettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
