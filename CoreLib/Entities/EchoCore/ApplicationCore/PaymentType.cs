using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore;

public class PaymentType : BaseEntity<uint>
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public ICollection<PaymentMethod>? PaymentMethods { get; set; }
}
