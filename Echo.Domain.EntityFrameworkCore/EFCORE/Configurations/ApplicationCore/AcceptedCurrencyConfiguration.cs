using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.ApplicationCore;

public class AcceptedCurrencyConfiguration : IEntityTypeConfiguration<AcceptedCurrency> //needs default currencies ("dkk"), ("eur"), ("usd")
{
    public string Name { get; set; }

    public ICollection<Subscription>? Subscriptions { get; set; }
    public ICollection<SubscriptionTransactionGroup>? TransactionGroups { get; set; }
    public ICollection<SubscriptionTransaction>? Transactions { get; set; }

    public void Configure(EntityTypeBuilder<AcceptedCurrency> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name);

        builder.HasMany(b => b.Subscriptions).WithOne(b => b.Currency).HasForeignKey(b => b.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.TransactionGroups).WithOne(b => b.Currency).HasForeignKey(b => b.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(b => b.Transactions).WithOne(b => b.Currency).HasForeignKey(b => b.CurrencyId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new AcceptedCurrency { Id = 1, Name = "dkk" });
        builder.HasData(new AcceptedCurrency { Id = 2, Name = "eur" });
        builder.HasData(new AcceptedCurrency { Id = 3, Name = "usd" });
    }
}