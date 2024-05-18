using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore
{
    public class SubscriptionTransactionRefundConfiguration : IEntityTypeConfiguration<SubscriptionTransactionRefund>
    {
        public void Configure(EntityTypeBuilder<SubscriptionTransactionRefund> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b=>b.Transaction).WithOne(b=>b.Refund).HasForeignKey<SubscriptionTransaction>(b=>b.Id).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
