using CoreLib.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessageReportReasonConfiguration : IEntityTypeConfiguration<ChatMessageReportReason>
    {
        public void Configure(EntityTypeBuilder<ChatMessageReportReason> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.Reason)
                .IsRequired();
            builder
                .Property(b => b.Description)
                .IsRequired();
            builder.HasMany(b => b.Reports).WithMany(e => e.Reasons);
        }
    }
}