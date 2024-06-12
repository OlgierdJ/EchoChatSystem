using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.Content)
            .IsRequired();
        builder
            .Property(b => b.TimeSent).HasDefaultValueSql("getdate()").IsRequired();

        builder.HasOne(b => b.Author).WithMany(e => e.ChatMessages).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

        builder.HasOne(b => b.MessageHolder).WithMany(e => e.Messages).HasForeignKey(b => b.MessageHolderId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasOne(b => b.Parent).WithMany(e => e.Children).HasForeignKey(b => b.ParentId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        //builder.HasMany(b => b.Reports).WithOne(e => e.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

        builder.HasMany(b => b.Attachments).WithOne(e => e.Message).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
        builder.HasMany(b => b.MessageTrackers).WithOne(e => e.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        builder.HasOne(b => b.MessagePin).WithOne(e => e.Message).HasForeignKey<ChatMessagePin>(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
    }
}
