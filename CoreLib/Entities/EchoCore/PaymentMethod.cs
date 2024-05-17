using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;

namespace CoreLib.Entities.EchoCore
{
    public class PaymentMethod : BaseEntity<ulong> //owner id
    {
        public uint PaymentTypeId { get; set; } //paypal, creditcard, etc
        public DateTime TimeAdded { get; set; }
        public bool IsDefaultPaymentMethod { get; set; } //can only be one per table

        //not fully implemented cause havent implemented payment system integration
        //Everything should be stored safely

        //public uint CountryId { get; set; }
        //public string? ExternalTransactionToken { get; set; }
        //public string CardNumber { get; set; }
        //public string NameOnCard { get; set; }
        //public string ExpirationDateMMYY { get; set; }
        //public string CVC { get; set; }
        //public string Address { get; set; }
        //public string? Address2 { get; set; }
        //public string City { get; set; }
        //public string CountyRegion { get; set; }
        //public string PostalCode { get; set; }

        //public Country Country { get; set; }

        //public PaypalInformation PaypalInformation { get; set; }
        //public StripeInformation StripeInformation { get; set; }
        //public CreditCardInformation CreditCardInformation { get; set; }
        public PaymentType Type { get; set; }
        public BillingInformation BillingInformation { get; set; }
        //public ICollection<Payment>
    }
}
