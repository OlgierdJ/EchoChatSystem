using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelAccountMessageTrackerConfiguration : IEntityTypeConfiguration<ServerTextChannelAccountMessageTracker>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelAccountMessageTracker> builder)
        {
            builder.HasKey(b => new { b.OwnerId, b.CoOwnerId, b.SubjectId });

            builder.HasOne(b => b.Owner).WithMany(b => b.TextChannelMessageTrackers).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
            builder.HasOne(b => b.CoOwner).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
            builder.HasOne(b => b.Subject).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired(false);
        }
    }
}
