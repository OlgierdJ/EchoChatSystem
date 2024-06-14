using Echo.Domain.Shared.Entities.EchoCore.ReportCore.CustomStatus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ReportCore.CustomStatus;

public class CustomStatusReportConfiguration : IEntityTypeConfiguration<CustomStatusReport>
{
    public void Configure(EntityTypeBuilder<CustomStatusReport> builder)
    {
        builder
            .HasKey(b => b.Id);
        builder
            .Property(b => b.Message);
        builder
          .Property(b => b.TimeSent).HasDefaultValueSql("getdate()").IsRequired();
        builder.HasOne(e => e.Reporter).WithMany().HasForeignKey(e => e.ReporterId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(e => e.Subject).WithOne(e => e.Report).HasForeignKey<CustomStatusReport>(e => e.SubjectId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasMany(e => e.Reasons).WithMany(e => e.Reports);
        //builder.HasMany(b => b.Participants).WithMany(e => e.Friendships).UsingEntity<FriendshipParticipancy>(j =>
        //{
        //    j.HasOne(e => e.Friendship).WithMany().HasForeignKey(e => e.FriendshipId);
        //    j.HasOne(e => e.Account).WithMany().HasForeignKey(e => e.AccountId);
        //});
    }
}
