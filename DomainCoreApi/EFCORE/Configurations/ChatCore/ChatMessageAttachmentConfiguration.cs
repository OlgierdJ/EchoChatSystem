using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ChatCore
{
    public class ChatMessageAttachmentConfiguration : IEntityTypeConfiguration<ChatMessageAttachment>
    {
        public void Configure(EntityTypeBuilder<ChatMessageAttachment> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.FileLocationURL)
                .IsRequired();
            builder.HasOne(b => b.Message).WithMany(e => e.Attachments).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

        }
    }
}
