using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    //public class ChatMessageReportConfiguration : IEntityTypeConfiguration<ChatMessageReport>
    //{
    //    public void Configure(EntityTypeBuilder<ChatMessageReport> builder)
    //    {
    //        builder
    //            .HasKey(b => b.Id);
    //        builder
    //            .Property(b => b.Message)
    //            .IsRequired();
    //        builder
    //            .Property(b => b.TimeSent).ValueGeneratedOnAdd()
    //            .IsRequired();
    //        builder.HasOne(b => b.Reporter).WithMany(e => e.ChatMessageReports).HasForeignKey(b => b.ReporterId).OnDelete(DeleteBehavior.Restrict).IsRequired(); 
    //        builder.HasOne(b => b.Subject).WithMany(e => e.Reports).HasForeignKey(b => b.SubjectId).OnDelete(DeleteBehavior.Cascade).IsRequired();
    //        builder.HasMany(b => b.Reasons).WithMany(e => e.Reports);
    //    }
    //}
}
