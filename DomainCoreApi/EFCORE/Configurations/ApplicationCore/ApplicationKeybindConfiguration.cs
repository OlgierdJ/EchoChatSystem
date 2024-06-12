using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore;

public class ApplicationKeybindConfiguration : IEntityTypeConfiguration<ApplicationKeybind> //this need default values f.eks ("mute / unmute self", "mutes self if unmuted and unmutes self if muted")
{
    public void Configure(EntityTypeBuilder<ApplicationKeybind> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).IsRequired(); // not mapped most of stuff
        builder.HasIndex(b => b.Name).IsUnique(); // not mapped most of stuff
        builder.Property(b => b.Description).IsRequired(false); // not mapped most of stuff

        builder.HasMany(b => b.Keybinds).WithOne(e => e.ApplicationKeybind).HasForeignKey(b => b.ApplicationKeybindId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

        builder.HasData(new ApplicationKeybind { Id = 1, Name = "mute / unmute self", Description = "mutes self if unmuted and unmutes self if muted" });
        builder.HasData(new ApplicationKeybind { Id = 2, Name = "Edit message", Description = "Edit a message if you have the permission" });
        builder.HasData(new ApplicationKeybind { Id = 3, Name = "Pin message", Description = "Pin a message in a chat" });
        builder.HasData(new ApplicationKeybind { Id = 4, Name = "Reply", Description = "make a reply to a message in a chat" });
    }
}
