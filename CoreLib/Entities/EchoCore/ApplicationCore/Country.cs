using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore;

public class Country : BaseEntity<uint>
{
    public string Name { get; set; }
    public ICollection<PaymentMethod>? PaymentMethods { get; set; }
}