using CoreLib.Entities.EchoCore.ApplicationCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<ApplicationKeybind>
    {
        public void Configure(EntityTypeBuilder<ApplicationKeybind> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name).IsRequired(); // not mapped most of stuff
            builder.HasIndex(b => b.Name).IsUnique(); // not mapped most of stuff
            builder.Property(b => b.Description).IsRequired(false); // not mapped most of stuff

            builder.HasMany(b => b.Keybinds).WithOne(e => e.ApplicationKeybind).HasForeignKey(b => b.ApplicationKeybindId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
