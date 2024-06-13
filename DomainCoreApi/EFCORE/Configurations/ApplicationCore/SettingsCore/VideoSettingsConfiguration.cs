using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class VideoSettingsConfiguration : IEntityTypeConfiguration<VideoSettings>
{
    public void Configure(EntityTypeBuilder<VideoSettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.VideoSettings).HasForeignKey<VideoSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
