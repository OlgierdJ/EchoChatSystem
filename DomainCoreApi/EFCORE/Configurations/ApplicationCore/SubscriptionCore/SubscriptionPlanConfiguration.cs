using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore;

public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.Role).WithMany().HasForeignKey(b => b.RoleId);

        builder.HasMany(b => b.Subscriptions).WithOne(b => b.SubscriptionPlan).HasForeignKey(b => b.SubscriptionPlanId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.ActivePeriods).WithOne(b => b.SubscriptionPlan).HasForeignKey(b => b.SubscriptionPlanId).OnDelete(DeleteBehavior.Restrict);
    }
}
