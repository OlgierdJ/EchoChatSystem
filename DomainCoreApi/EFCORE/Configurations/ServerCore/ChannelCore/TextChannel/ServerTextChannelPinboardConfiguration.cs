using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelPinboardConfiguration : IEntityTypeConfiguration<ServerTextChannelPinboard>
    {
        public void Configure(EntityTypeBuilder<ServerTextChannelPinboard> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Owner).WithOne(b => b.Pinboard).HasForeignKey<ServerTextChannelPinboard>(b => b.OwnerId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(b => b.PinnedMessages).WithOne(b => b.Pinboard).HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
