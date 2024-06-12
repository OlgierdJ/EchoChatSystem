using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class AdvancedSettingsConfiguration : IEntityTypeConfiguration<AdvancedSettings>
{
    public void Configure(EntityTypeBuilder<AdvancedSettings> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.Property(b => b.DeveloperMode).IsRequired();
        builder.Property(b => b.UseHardwareAccelerationToMakeEchoSmoother).IsRequired();
        builder.Property(b => b.AutoNavigateServerHome).IsRequired();

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.AdvancedSettings).HasForeignKey<AdvancedSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
