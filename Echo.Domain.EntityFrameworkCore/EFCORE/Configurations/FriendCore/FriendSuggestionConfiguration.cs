using Echo.Domain.Shared.Entities.EchoCore.FriendCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.FriendCore;

public class FriendSuggestionConfiguration : IEntityTypeConfiguration<FriendSuggestion>
{
    public void Configure(EntityTypeBuilder<FriendSuggestion> builder)
    {
        builder
            .HasKey(b => new { b.ReceiverId, b.SuggestionId });
        builder
            .Property(b => b.TimeSuggested)
            .HasDefaultValueSql("getdate()")
            .IsRequired();
        builder
           .Property(b => b.Declined)
           .IsRequired();
        builder.HasOne(b => b.Suggestion)
            .WithMany()
            .HasForeignKey(b => b.SuggestionId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
        builder.HasOne(b => b.Receiver)
            .WithMany(e => e.FriendSuggestions)
            .HasForeignKey(b => b.ReceiverId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .IsRequired();
        builder.HasIndex(b => new { b.ReceiverId, b.SuggestionId }).IsUnique();
    }
}
