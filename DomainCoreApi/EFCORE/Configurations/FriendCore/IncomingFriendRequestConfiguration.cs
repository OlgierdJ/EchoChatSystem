using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class IncomingFriendRequestConfiguration : IEntityTypeConfiguration<IncomingFriendRequest>
    {
        public void Configure(EntityTypeBuilder<IncomingFriendRequest> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder.HasOne(b => b.Receiver)
                .WithMany(b => b.IncomingFriendRequests)
                .HasForeignKey(b => b.ReceiverId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();
            builder.HasOne(b => b.SenderRequest)
                .WithOne(b => b.ReceiverRequest)
                .HasForeignKey<IncomingFriendRequest>(e => e.SenderRequestId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .IsRequired();
        }
    }
}
