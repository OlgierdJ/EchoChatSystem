﻿using CoreLib.Entities.EchoCore.ReportCore.Feedback;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ReportCore.Feedback
{
    public class FeedbackReportReasonConfiguration : IEntityTypeConfiguration<FeedbackReportReason>
    {
        public void Configure(EntityTypeBuilder<FeedbackReportReason> builder)
        {
            builder
                .HasKey(b => b.Id);
            builder
                .Property(b => b.Reason);
            builder
               .Property(b => b.Description);
            builder.HasMany(e => e.Reports).WithMany(e => e.Reasons);
            //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(j =>
            //{
            //    j.HasOne(e => e.Friendship).WithMany().HasForeignKey(e => e.FriendshipId);
            //    j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
            //});
        }
    }
}