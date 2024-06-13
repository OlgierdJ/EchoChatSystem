using Echo.Domain.Shared.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore;

public class OutgoingFriendRequestConfiguration : IEntityTypeConfiguration<OutgoingFriendRequest>
{
    public void Configure(EntityTypeBuilder<OutgoingFriendRequest> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder.HasOne(b => b.Sender)
            .WithMany(b => b.OutgoingFriendRequests)
            .HasForeignKey(b => b.SenderId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
        builder.HasOne(b => b.ReceiverRequest)
            .WithOne(b => b.SenderRequest)
            .HasForeignKey<IncomingFriendRequest>(e => e.SenderRequestId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
    }
}
