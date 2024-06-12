using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore;

public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> builder)
    {
        builder.HasKey(b => b.Id);

        //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

        builder.HasMany(b => b.AppearanceSettings).WithOne(e => e.Theme).HasForeignKey(b => b.ThemeId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        builder.HasData(new Theme { Id = 1, Name = "Dark" });
        builder.HasData(new Theme { Id = 2, Name = "Light" });
    }
}
