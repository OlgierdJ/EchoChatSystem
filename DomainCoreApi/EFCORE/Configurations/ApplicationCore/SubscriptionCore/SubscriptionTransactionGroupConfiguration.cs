using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore;

public class SubscriptionTransactionGroupConfiguration : IEntityTypeConfiguration<SubscriptionTransactionGroup>
{
    public void Configure(EntityTypeBuilder<SubscriptionTransactionGroup> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.Currency).WithMany(b => b.TransactionGroups).HasForeignKey(b => b.CurrencyId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Transactions).WithOne(b => b.TransactionGroup).HasForeignKey(b => b.TransactionGroupId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.ActivePeriods).WithOne(b => b.TransactionGroup).HasForeignKey(b => b.TransactionGroupId).OnDelete(DeleteBehavior.Restrict);
    }
}
