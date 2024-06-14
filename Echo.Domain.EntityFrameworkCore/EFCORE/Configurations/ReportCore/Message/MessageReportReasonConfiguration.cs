﻿using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ReportCore.Message;

public class MessageReportReasonConfiguration : IEntityTypeConfiguration<MessageReportReason>
{
    public void Configure(EntityTypeBuilder<MessageReportReason> builder)
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