using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class StreamerModeSettingsConfiguration : IEntityTypeConfiguration<StreamerModeSettings>
{
    public void Configure(EntityTypeBuilder<StreamerModeSettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.StreamerModeSettings).HasForeignKey<StreamerModeSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
