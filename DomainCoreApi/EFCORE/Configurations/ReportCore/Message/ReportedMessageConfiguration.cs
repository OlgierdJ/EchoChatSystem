using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ReportCore.Message;

namespace DomainCoreApi.EFCORE.Configurations.ReportCore.Message
{
    public class ReportedMessageConfiguration : IEntityTypeConfiguration<ReportedMessage>
    {
        public void Configure(EntityTypeBuilder<ReportedMessage> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.Content)
                .IsRequired();
            builder
                .Property(b => b.MessageType)
                .IsRequired();
            builder
                .Property(b => b.TimeSent).HasDefaultValueSql("getdate()").IsRequired();

            builder.HasOne(b => b.Author).WithMany(e => e.ReportedMessages).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            //builder.HasMany(b => b.Reports).WithOne(e => e.Subject).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasMany(b => b.Attachments).WithOne(e => e.Message).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Report).WithOne(e => e.Subject).HasForeignKey<MessageReport>(b => b.SubjectId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
