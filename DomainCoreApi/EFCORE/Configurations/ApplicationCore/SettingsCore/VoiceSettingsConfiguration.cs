using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class VoiceSettingsConfiguration : IEntityTypeConfiguration<VoiceSettings>
{
    public void Configure(EntityTypeBuilder<VoiceSettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.VoiceSettings).HasForeignKey<VoiceSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
