using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatAccountMessageTrackerConfiguration : IEntityTypeConfiguration<ChatAccountMessageTracker>
        {
            public void Configure(EntityTypeBuilder<ChatAccountMessageTracker> builder)
            {
                builder
                    .HasKey(b => b.Id);

                builder.HasOne(b => b.Owner).WithMany(b => b.ChatMessageTrackers).HasForeignKey(b => b.OwnerId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
                builder.HasOne(b => b.Subject).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
                builder.HasOne(b => b.Holder).WithMany(b => b.MessageTrackers).HasForeignKey(b => b.HolderId).OnDelete(DeleteBehavior.Cascade).IsRequired();
               
            }
        
    }
}
