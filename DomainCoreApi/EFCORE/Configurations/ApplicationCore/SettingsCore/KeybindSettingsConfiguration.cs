using CoreLib.Entities.EchoCore.ApplicationCore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.SettingsCore
{
    public class KeybindSettingsConfiguration : IEntityTypeConfiguration<KeybindSettings>
    {
        public void Configure(EntityTypeBuilder<KeybindSettings> builder)
        {
            builder.HasKey(b => b.Id);

            //builder.Property(b => b.SaturationPercent).IsRequired(); // not mapped most of stuff

            builder.HasOne(b => b.AccountSettings).WithOne(e => e.KeybindSettings).HasForeignKey<KeybindSettings>(b => b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasMany(e => e.Keybinds).WithOne(e => e.KeybindSettings).HasForeignKey(e => e.KeybindSettingsId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
