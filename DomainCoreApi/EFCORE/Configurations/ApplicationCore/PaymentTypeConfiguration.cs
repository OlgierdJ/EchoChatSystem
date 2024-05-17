using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class PaymentTypeConfiguration : BaseEntity<uint>
    {
        public string Name { get; set; }
        public ICollection<PaymentMethodConfiguration>? PaymentMethods { get; set; }
    }
}
