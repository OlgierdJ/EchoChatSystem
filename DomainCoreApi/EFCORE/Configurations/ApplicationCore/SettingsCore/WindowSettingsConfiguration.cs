using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.Settings;

public class WindowSettingsConfiguration : IEntityTypeConfiguration<WindowSettings>
{
    public void Configure(EntityTypeBuilder<WindowSettings> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.WindowSettings).HasForeignKey<WindowSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
