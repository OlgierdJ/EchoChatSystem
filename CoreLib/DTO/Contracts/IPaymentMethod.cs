using CoreLib.DTO.EchoCore.UserCore.SubscriptionCore;

namespace CoreLib.DTO.Contracts
{
    public interface IPaymentMethod
    {
        string Description { get; set; }
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        bool IsDefaultMethod { get; set; }
        DateTime TimeAdded { get; set; }
        PaymentTypeDTO Type { get; set; }
    }
}
