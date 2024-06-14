using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ChatCore;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.Name)
            .IsRequired();
        builder
           .Property(b => b.IconUrl)
           .IsRequired(false);
        builder
            .Property(b => b.TimeCreated)
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder.HasMany(b => b.MessageTrackers)
            .WithOne(b => b.CoOwner)
            .HasForeignKey(b => b.CoOwnerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(b => b.Messages)
            .WithOne(b => b.MessageHolder)
            .HasForeignKey(b => b.MessageHolderId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasMany(b => b.Muters).WithOne(b => b.Subject)
            .HasForeignKey(b => b.SubjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(b => b.Invites).WithOne(b => b.Subject)
            .HasForeignKey(b => b.SubjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.HasOne(b => b.DirectMessageRelation).WithOne(b => b.Chat)
            .HasForeignKey<DirectMessageRelation>(b => b.ChatId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        builder.HasMany(b => b.Participants).WithOne(b => b.Subject)
            .HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        builder.HasMany(b => b.PinnedMessages).WithOne(b => b.Pinboard)
            .HasForeignKey(b => b.PinboardId).OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
