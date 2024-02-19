using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoreLib.Entities.EchoCore.FriendCore;

namespace DomainCoreApi.EFCORE.Configurations.FriendCore
{
    public class FriendSuggestionConfiguration : IEntityTypeConfiguration<FriendSuggestion>
    {
        public void Configure(EntityTypeBuilder<FriendSuggestion> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.TimeCreated)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder
               .Property(b => b.Declined)
               .IsRequired();
            builder.HasOne(b => b.SuggestedFriend)
                .WithMany()
                .HasForeignKey(b => b.SuggestedFriendId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasOne(b => b.Account)
                .WithMany(e => e.FriendSuggestions)
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            builder.HasIndex(b=>new { b.AccountId, b.SuggestedFriendId }).IsUnique();
        }
    }
}
