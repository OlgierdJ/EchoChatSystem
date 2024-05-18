using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class CountryConfiguration : BaseEntity<uint> //add default values for different kinds of countries f.eks danmark, svarige, deutschland, england, france, zhonghuan, etc USA US A U S A
    {
        public string Name { get; set; }
        public ICollection<PaymentMethodConfiguration>? PaymentMethods { get; set; }
    }
}