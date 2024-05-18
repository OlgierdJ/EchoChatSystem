using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatAccountMessageTrackerConfiguration : IEntityTypeConfiguration<ChatAccountMessageTracker>
    {
        public void Configure(EntityTypeBuilder<ChatAccountMessageTracker> builder)
        {
            builder
                .HasKey(b => new { b.OwnerId, b.CoOwnerId });

            builder.HasOne(b => b.Owner).WithMany(b => b.ChatMessageTrackers).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.CoOwner).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.CoOwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Subject).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

        }

    }
}
