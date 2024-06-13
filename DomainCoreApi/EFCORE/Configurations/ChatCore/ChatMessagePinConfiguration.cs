using Echo.Domain.Shared.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore;

public class ChatMessagePinConfiguration : IEntityTypeConfiguration<ChatMessagePin>
{
    public void Configure(EntityTypeBuilder<ChatMessagePin> builder)
    {
        builder
            .HasKey(b => new { b.PinboardId, b.MessageId });
        builder.HasOne(b => b.Pinboard).WithMany(e => e.PinnedMessages).HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.NoAction).IsRequired(); //probably better to ignore pinboard as messages can actually get deleted
        //builder.Ignore(e=>e.Pinboard);


        builder.HasOne(b => b.Message).WithOne(e => e.MessagePin).HasForeignKey<ChatMessagePin>(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
    }
}