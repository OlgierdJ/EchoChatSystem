using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreLib.Entities.EchoCore;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod> //owner id
{
    public uint PaymentTypeId { get; set; } //paypal, creditcard, etc
    public DateTime TimeAdded { get; set; }
    public bool IsDefaultPaymentMethod { get; set; } //can only be one per table
    public PaymentType Type { get; set; }
    public BillingInformation BillingInformation { get; set; }

    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.TimeAdded).HasDefaultValueSql("getdate()");
        builder.Property(b => b.IsDefaultMethod);

        builder.HasOne(b => b.Type).WithMany(b => b.PaymentMethods).HasForeignKey(b => b.PaymentTypeId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.BillingInformation).WithMany(b => b.PaymentMethods).HasForeignKey(b => b.Id).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(b => b.Country).WithMany(b => b.PaymentMethods).HasForeignKey(b => b.CountryId).OnDelete(DeleteBehavior.Restrict);
    }
}
