using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatMessagePinConfiguration : IEntityTypeConfiguration<ChatMessagePin>
    {
        public void Configure(EntityTypeBuilder<ChatMessagePin> builder)
        {
            builder
                .HasKey(b => new { b.PinboardId, b.MessageId });
            builder.HasOne(b => b.Pinboard).WithMany(e => e.PinnedMessages).HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();


            builder.HasOne(b => b.Message).WithOne(e => e.MessagePin).HasForeignKey<ChatMessagePin>(b => b.MessageId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}