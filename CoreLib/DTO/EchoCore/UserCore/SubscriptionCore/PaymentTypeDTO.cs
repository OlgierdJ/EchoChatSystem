namespace CoreLib.DTO.EchoCore.UserCore.SubscriptionCore
{
    public interface IPaymentType
    {
        string Icon { get; set; }
        uint Id { get; set; }
        string Name { get; set; }
    }

    public class PaymentTypeDTO : IPaymentType
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
