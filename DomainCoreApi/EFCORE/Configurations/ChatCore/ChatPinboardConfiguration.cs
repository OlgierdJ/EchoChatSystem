using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatPinboardConfiguration : IEntityTypeConfiguration<ChatPinboard>
    {
        public void Configure(EntityTypeBuilder<ChatPinboard> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder.HasOne(b => b.Owner).WithOne(b => b.Pinboard).HasForeignKey<ChatPinboard>(b => b.OwnerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasMany(b => b.PinnedMessages).WithOne(b => b.Pinboard).HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
