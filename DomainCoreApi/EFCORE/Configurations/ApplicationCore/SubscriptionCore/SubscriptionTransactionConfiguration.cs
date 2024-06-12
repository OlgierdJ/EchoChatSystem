using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore;

public class SubscriptionTransactionConfiguration : IEntityTypeConfiguration<SubscriptionTransaction>
{
    public void Configure(EntityTypeBuilder<SubscriptionTransaction> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.TransactionType).WithMany().HasForeignKey(b => b.TransactionTypeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Currency).WithMany(b => b.Transactions).HasForeignKey(b => b.CurrencyId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.TransactionGroup).WithMany(b => b.Transactions).HasForeignKey(b => b.TransactionGroupId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Refund).WithOne(b => b.Transaction).HasForeignKey<SubscriptionTransactionRefund>(b => b.Id).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Subscription).WithMany(b => b.SubcriptionTransactions).HasForeignKey(b => b.SubscriptionId).OnDelete(DeleteBehavior.Restrict);
    }
}
