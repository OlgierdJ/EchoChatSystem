using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore
{
    public class AppearanceSettingsConfiguration : IEntityTypeConfiguration<AppearanceSettings>
    {
        public void Configure(EntityTypeBuilder<AppearanceSettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff
            builder.Property(b=>b.InAppIcon).HasDefaultValue("").IsRequired();
            builder.Property(b => b.DarkSideBar).IsRequired();
            builder.Property(b => b.MessageDisplayMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.ShowAvatarsInCompactMode).IsRequired();
            builder.Property(b => b.PixelChatFontScale).IsRequired();
            builder.Property(b => b.PixelSpaceBetweenMessageGroupsScale).IsRequired();
            builder.Property(b => b.ZoomLevel).IsRequired();

            builder.HasOne(b => b.Theme).WithMany(b=>b.AppearanceSettings).HasForeignKey(b=>b.ThemeId).IsRequired();
            builder.HasOne(b => b.AccountSettings).WithOne(e => e.AppearanceSettings).HasForeignKey<AppearanceSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }

}
