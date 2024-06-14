using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ReportCore.Profile;

public class ProfileReportReasonConfiguration : IEntityTypeConfiguration<ProfileReportReason>
{
    public void Configure(EntityTypeBuilder<ProfileReportReason> builder)
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
