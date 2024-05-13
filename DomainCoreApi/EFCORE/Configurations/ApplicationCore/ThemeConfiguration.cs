using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ApplicationCore;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
    {
        public void Configure(EntityTypeBuilder<Theme> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasMany(b => b.AppearanceSettings).WithOne(e => e.Theme).HasForeignKey(b => b.ThemeId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasData(new Theme {Id=1, Name="Dark" });
            builder.HasData(new Theme {Id=2, Name = "Light" });
        }
    }
}
