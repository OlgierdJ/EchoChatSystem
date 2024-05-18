using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class PaymentTypeConfiguration : BaseEntity<uint> //needs supported payment systems f.eks ("PayPal"), ("MobilePay"), ("Card"), ("Stripe"), ("PaysafeCard"), ("Google Pay")
    {
        public string Name { get; set; }
        public ICollection<PaymentMethodConfiguration>? PaymentMethods { get; set; }
    }
}
