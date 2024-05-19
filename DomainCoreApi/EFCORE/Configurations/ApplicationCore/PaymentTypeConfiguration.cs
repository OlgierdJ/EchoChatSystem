using CoreLib.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType> //needs supported payment systems f.eks ("PayPal"), ("MobilePay"), ("Card"), ("Stripe"), ("PaysafeCard"), ("Google Pay")
    {
        public string Name { get; set; }
        public ICollection<PaymentMethodConfiguration>? PaymentMethods { get; set; }

        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Name);

            builder.HasMany(x => x.PaymentMethods).WithOne(b => b.Type).HasForeignKey(b => b.PaymentTypeId).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasData(new PaymentType { Id = 1, Name = "PayPal" });
            builder.HasData(new PaymentType { Id = 2, Name = "MobilePay" });
            builder.HasData(new PaymentType { Id = 3, Name = "PaysafeCard" });
            builder.HasData(new PaymentType { Id = 4, Name = "Visa" });
            builder.HasData(new PaymentType { Id = 5, Name = "MasterCard" });
            builder.HasData(new PaymentType { Id = 6, Name = "Google Pay" });
            builder.HasData(new PaymentType { Id = 7, Name = "Apple Pay" });
            builder.HasData(new PaymentType { Id = 8, Name = "Stripe" });
        }
    }
}
