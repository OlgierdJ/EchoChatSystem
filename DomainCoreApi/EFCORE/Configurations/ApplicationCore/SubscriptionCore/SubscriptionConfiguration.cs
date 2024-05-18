using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b=>b.BillingInformation).WithMany(b => b.Subscriptions).HasForeignKey(b=>b.BillingInformationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.SubscriptionPlan).WithMany(b => b.Subscriptions).HasForeignKey(b => b.SubscriptionPlanId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Currency).WithMany(b => b.Subscriptions).HasForeignKey(b => b.CurrencyId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.SubcriptionTransactions).WithOne(b => b.Subscription).HasForeignKey(b => b.SubscriptionId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
        }
    }
}
