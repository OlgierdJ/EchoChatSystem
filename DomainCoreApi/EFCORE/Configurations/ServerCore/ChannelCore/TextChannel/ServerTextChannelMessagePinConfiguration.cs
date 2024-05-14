using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelMessagePinConfiguration : IEntityTypeConfiguration<ServerTextChannelMessagePin>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelMessagePin> builder)
        {
            builder.HasKey(b => new { b.PinboardId, b.MessageId });

            builder.HasOne(b=>b.Pinboard).WithMany(b=>b.PinnedMessages).HasForeignKey(b=>b.PinboardId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Message).WithOne(b => b.MessagePin).HasForeignKey<ServerTextChannelMessagePin>(b => b.MessageId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}