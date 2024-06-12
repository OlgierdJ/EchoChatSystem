using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore;

public class SubscriptionPlanActivePeriodConfiguration : IEntityTypeConfiguration<SubscriptionPlanActivePeriod>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlanActivePeriod> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasOne(b => b.SubscriptionPlan).WithMany(b => b.ActivePeriods).HasForeignKey(b => b.SubscriptionPlanId).OnDelete(DeleteBehavior.Restrict);
    }
}
