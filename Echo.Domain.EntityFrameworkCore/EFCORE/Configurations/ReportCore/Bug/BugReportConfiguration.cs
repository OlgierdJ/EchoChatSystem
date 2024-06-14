using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Bug;
using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Bug;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ReportCore.Bug;

public class BugReportConfiguration : IEntityTypeConfiguration<BugReport>
{
    public void Configure(EntityTypeBuilder<BugReport> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.Message);
        builder
          .Property(b => b.TimeSent).HasDefaultValueSql("getdate()").IsRequired();
        builder.HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasMany(e => e.Reasons).WithMany(e => e.Reports);
        //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(j =>
        //{
        //    j.HasOne(e => e.Friendship).WithMany().HasForeignKey(e => e.FriendshipId);
        //    j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
        //});
    }
}
