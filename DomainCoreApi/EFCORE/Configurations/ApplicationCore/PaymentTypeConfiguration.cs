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
            builder.Property(b => b.Icon);

            builder.HasMany(x => x.PaymentMethods).WithOne(b => b.Type).HasForeignKey(b => b.PaymentTypeId).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasData(new PaymentType { Id = 1, Name = "PayPal",Icon= "https://upload.wikimedia.org/wikipedia/commons/b/b5/PayPal.svg" });
            builder.HasData(new PaymentType { Id = 2, Name = "MobilePay",Icon= "https://upload.wikimedia.org/wikipedia/fi/f/fd/MobilePay_logo.svg" });
            builder.HasData(new PaymentType { Id = 3, Name = "PaysafeCard",Icon= "https://upload.wikimedia.org/wikipedia/commons/e/e8/Paysafe.svg" });
            builder.HasData(new PaymentType { Id = 4, Name = "Visa", Icon= "https://upload.wikimedia.org/wikipedia/commons/5/5e/Visa_Inc._logo.svg" });
            builder.HasData(new PaymentType { Id = 5, Name = "MasterCard", Icon= "https://upload.wikimedia.org/wikipedia/commons/2/2a/Mastercard-logo.svg" });
            builder.HasData(new PaymentType { Id = 6, Name = "Google Pay", Icon = "https://upload.wikimedia.org/wikipedia/commons/f/f2/Google_Pay_Logo.svg" });
            builder.HasData(new PaymentType { Id = 7, Name = "Apple Pay", Icon = "https://upload.wikimedia.org/wikipedia/commons/b/b0/Apple_Pay_logo.svg" });
            builder.HasData(new PaymentType { Id = 8, Name = "Stripe", Icon = "https://upload.wikimedia.org/wikipedia/commons/b/ba/Stripe_Logo%2C_revised_2016.svg" });
        }
    }
}
