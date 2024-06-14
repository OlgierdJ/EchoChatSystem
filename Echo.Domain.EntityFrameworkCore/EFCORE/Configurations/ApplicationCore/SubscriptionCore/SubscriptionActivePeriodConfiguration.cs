using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore.SubscriptionCore;

public class SubscriptionActivePeriodConfiguration : IEntityTypeConfiguration<SubscriptionActivePeriod>
{
    public void Configure(EntityTypeBuilder<SubscriptionActivePeriod> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.StartTime).HasDefaultValueSql("getdate()").IsRequired();
        builder.HasOne(b => b.TransactionGroup).WithMany(b => b.ActivePeriods).HasForeignKey(b => b.TransactionGroupId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Subscription).WithMany().HasForeignKey(b => b.SubcriptionId).OnDelete(DeleteBehavior.Restrict);
    }
}
