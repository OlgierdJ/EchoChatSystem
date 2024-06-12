using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ReportCore.CustomStatus;

public class ReportedCustomStatusConfiguration : IEntityTypeConfiguration<ReportedCustomStatus>
{
    public void Configure(EntityTypeBuilder<ReportedCustomStatus> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.CustomMessage);
        builder.HasOne(e => e.Account).WithMany(e => e.ReportedCustomStatuses).HasForeignKey(e => e.AccountId);
        builder.HasOne(e => e.Report).WithOne(e => e.Subject).HasForeignKey<CustomStatusReport>(e => e.SubjectId);
        //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(j =>
        //{
        //    j.HasOne(e => e.Friendship).WithMany().HasForeignKey(e => e.FriendshipId);
        //    j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
        //});
    }
}
