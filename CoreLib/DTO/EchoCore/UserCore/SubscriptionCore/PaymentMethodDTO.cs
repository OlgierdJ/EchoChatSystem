namespace CoreLib.DTO.EchoCore.UserCore.SubscriptionCore
{
    public interface IPaymentMethod
    {
        string Description { get; set; }
        ulong Id { get; set; }
        bool IsDefaultMethod { get; set; }
        DateTime TimeAdded { get; set; }
        PaymentTypeDTO Type { get; set; }
    }

    public class PaymentMethodDTO : IPaymentMethod
    {
        public ulong Id { get; set; }
        public PaymentTypeDTO Type { get; set; }
        //public BillingAddressDTO Type { get; set; } //perhaps make this entity
        public DateTime TimeAdded { get; set; }
        public bool IsDefaultMethod { get; set; }
        public string Description { get; set; } //paypal email, name on card, etc
    }
}
