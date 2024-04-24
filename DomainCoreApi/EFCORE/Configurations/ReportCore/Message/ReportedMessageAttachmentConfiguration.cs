using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ChatCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoreLib.Entities.EchoCore.ReportCore.Message
{
    public class ReportedMessageAttachmentConfiguration : IEntityTypeConfiguration<ReportedMessageAttachment>
    {
        public void Configure(EntityTypeBuilder<ReportedMessageAttachment> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.FileURL)
                .IsRequired();
            builder.HasOne(b => b.Message).WithMany(e => e.Attachments).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();

        }
    }
}