using CoreLib.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class OutgoingFriendRequestConfiguration : IEntityTypeConfiguration<OutgoingFriendRequest>
    {
        public void Configure(EntityTypeBuilder<OutgoingFriendRequest> builder)
        {
            builder
                .HasKey(b => b.Id);

            builder.HasOne(b => b.Sender)
                .WithMany(b => b.OutgoingFriendRequests)
                .HasForeignKey(b => b.SenderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(b => b.ReceiverRequest)
                .WithOne(b => b.SenderRequest)
                .HasForeignKey<IncomingFriendRequest>(e => e.SenderRequestId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(); 
        }
    }
}
