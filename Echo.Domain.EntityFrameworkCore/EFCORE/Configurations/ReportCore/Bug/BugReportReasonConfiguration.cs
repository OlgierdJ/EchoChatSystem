using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Bug;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ReportCore.Bug;

public class BugReportReasonConfiguration : IEntityTypeConfiguration<BugReportReason>
{
    public void Configure(EntityTypeBuilder<BugReportReason> builder)
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
