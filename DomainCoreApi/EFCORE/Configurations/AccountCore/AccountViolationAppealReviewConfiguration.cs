using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountViolationAppealReviewConfiguration : IEntityTypeConfiguration<AccountViolationAppealReview>
    {
        public void Configure(EntityTypeBuilder<AccountViolationAppealReview> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.IsDenied).IsRequired();

            builder.HasOne(b => b.Reviewer).WithMany(e => e.ReviewedAppeals).HasForeignKey(b => b.ReviewerId).OnDelete(DeleteBehavior.Cascade).IsRequired();
            builder.HasOne(b => b.Appeal).WithOne(e => e.Review).HasForeignKey<AccountViolationAppealReview>(e => e.AppealId).OnDelete(DeleteBehavior.Cascade).IsRequired();

        }
    }
}
