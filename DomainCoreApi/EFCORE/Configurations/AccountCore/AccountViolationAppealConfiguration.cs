using CoreLib.Entities.EchoCore.AccountCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.AccountCore
{
    public class AccountViolationAppealConfiguration : IEntityTypeConfiguration<AccountViolationAppeal>
    {
        public void Configure(EntityTypeBuilder<AccountViolationAppeal> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Message).HasMaxLength(512).IsRequired();

            builder.HasOne(b => b.Violation).WithOne(e => e.Appeal).HasForeignKey<AccountViolationAppeal>(e => e.ViolationId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
            builder.HasOne(b => b.Review).WithOne(e => e.Appeal).HasForeignKey<AccountViolationAppealReview>(e => e.AppealId).OnDelete(DeleteBehavior.ClientCascade).IsRequired();
        }
    }
}
