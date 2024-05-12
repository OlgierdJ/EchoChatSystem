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
    public class KeybindConfiguration : IEntityTypeConfiguration<Keybind>
    {
        public void Configure(EntityTypeBuilder<Keybind> builder)
        {
            builder.HasKey(b => new {b.KeybindSettingsId, b.ApplicationKeybindId});

            builder.Property(b => b.Action).IsRequired(false); // not mapped most of stuff
            builder.HasIndex(b => b.Action).IsUnique(); // not mapped most of stuff

            builder.HasOne(b => b.ApplicationKeybind).WithMany(e => e.Keybinds).HasForeignKey(b => b.ApplicationKeybindId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne(e => e.KeybindSettings).WithMany(e => e.Keybinds).HasForeignKey(e => e.KeybindSettingsId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
