using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using CoreLib.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.Settings
{
    public class GameOverlaySettingsConfiguration : IEntityTypeConfiguration<GameOverlaySettings>
    {
        public void Configure(EntityTypeBuilder<GameOverlaySettings> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b=>b.EnableGameOverlay).IsRequired();
            builder.Property(b => b.EnableGameOverlay).HasDefaultValue("Shift + L").IsRequired();
            builder.Property(b => b.AvatarSizeMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.DisplayNamesMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.DisplayUsersMode).HasConversion<int>().IsRequired();
            builder.Property(b => b.OverlayNotificationsPlacement).HasConversion<int>().IsRequired();
            builder.Property(b => b.ShowTextChatNotifications).IsRequired();

            builder.HasOne(b => b.AccountSettings).WithOne().HasForeignKey<GameOverlaySettings>(b=>b.Id);
        }
    }
}
