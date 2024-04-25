using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ReportCore.CustomStatus
{
    public class ReportedCustomStatusConfiguration : IEntityTypeConfiguration<ReportedCustomStatus>
    {
        public void Configure(EntityTypeBuilder<ReportedCustomStatus> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.CustomMessage);
            builder.HasOne(e => e.Account).WithMany(e => e.ReportedCustomStatuses).HasForeignKey(e=>e.AccountId);
            builder.HasOne(e => e.Report).WithOne(e => e.Subject).HasForeignKey<CustomStatusReport>(e=>e.SubjectId);
            //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(j =>
            //{
            //    j.HasOne(e => e.Friendship).WithMany().HasForeignKey(e => e.FriendshipId);
            //    j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
            //});
        }
    }
}
