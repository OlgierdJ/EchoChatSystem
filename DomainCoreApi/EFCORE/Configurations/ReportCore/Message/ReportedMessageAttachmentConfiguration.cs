using CoreLib.Entities.EchoCore.ReportCore.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ReportCore.Message;

public class ReportedMessageAttachmentConfiguration : IEntityTypeConfiguration<ReportedMessageAttachment>
{
    public void Configure(EntityTypeBuilder<ReportedMessageAttachment> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.FileLocationURL)
            .IsRequired();
        builder.HasOne(b => b.Message).WithMany(e => e.Attachments).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();

    }
}