using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SettingsCore;

public class WindowSettingsConfiguration : IEntityTypeConfiguration<WindowSettings>
{
    public void Configure(EntityTypeBuilder<WindowSettings> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.AccountSettings).WithOne(e => e.WindowSettings).HasForeignKey<WindowSettings>(b => b.Id).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
    }
}
