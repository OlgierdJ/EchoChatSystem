using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

public class PaymentType : BaseEntity<uint>
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public ICollection<PaymentMethod>? PaymentMethods { get; set; }
}
