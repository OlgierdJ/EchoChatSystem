using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SubscriptionCore;


public class PaymentTypeDTO : IPaymentType
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}
