using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessagePinConfiguration : IEntityTypeConfiguration<ChatMessagePin>
    {
        public void Configure(EntityTypeBuilder<ChatMessagePin> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.TimePinned).ValueGeneratedOnAdd()
                .IsRequired();
            builder.HasOne(b => b.Pinboard).WithMany(e => e.PinnedMessages).HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
       

            builder.HasOne(b => b.Message).WithOne(e => e.MessagePin).HasForeignKey<ChatMessagePin>(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}