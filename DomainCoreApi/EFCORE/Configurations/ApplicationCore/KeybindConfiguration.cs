using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class KeybindConfiguration : IEntityTypeConfiguration<Keybind>
    {
        public void Configure(EntityTypeBuilder<Keybind> builder)
        {
            builder.HasKey(b => new { b.KeybindSettingsId, b.ApplicationKeybindId });

            builder.Property(b => b.Action).IsRequired(false); // not mapped most of stuff
            builder.HasIndex(b => b.Action).IsUnique(); // not mapped most of stuff

            builder.HasOne(b => b.ApplicationKeybind).WithMany(e => e.Keybinds).HasForeignKey(b => b.ApplicationKeybindId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

            builder.HasOne(e => e.KeybindSettings).WithMany(e => e.Keybinds).HasForeignKey(e => e.KeybindSettingsId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
