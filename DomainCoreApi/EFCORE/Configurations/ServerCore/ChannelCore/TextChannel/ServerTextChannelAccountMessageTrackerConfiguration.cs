using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelAccountMessageTrackerConfiguration : IEntityTypeConfiguration<AccountServerTextChannelMessageTracker>
{
    public void Configure(EntityTypeBuilder<AccountServerTextChannelMessageTracker> builder)
    {
        builder.HasKey(b => new { b.OwnerId, b.CoOwnerId });

        builder.HasOne(b => b.Owner).WithMany(b => b.TextChannelMessageTrackers).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasOne(b => b.CoOwner).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(b => b.Subject).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
    }
}
