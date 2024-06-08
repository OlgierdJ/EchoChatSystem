namespace CoreLib.DTO.Contracts
{
    public interface ISubscriptionTransactionRefund
    {
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        string Reason { get; set; }
        DateTime TimeRefunded { get; set; }
    }
}
