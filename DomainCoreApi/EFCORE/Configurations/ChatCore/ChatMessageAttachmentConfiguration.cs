using CoreLib.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessageAttachmentConfiguration : IEntityTypeConfiguration<ChatMessageAttachment>
    {
        public void Configure(EntityTypeBuilder<ChatMessageAttachment> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.FileURL)
                .IsRequired();
            builder.HasOne(b => b.Message).WithMany(e=>e.Attachments).HasForeignKey(b => b.MessageId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            
        }
    }
}
