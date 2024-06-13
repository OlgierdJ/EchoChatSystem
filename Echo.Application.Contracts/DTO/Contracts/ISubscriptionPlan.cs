namespace Echo.Application.Contracts.DTO.Contracts;

public interface ISubscriptionPlan
{
    double CostEUR { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    string Name { get; set; }
    string PaymentPlan { get; set; }
    string Type { get; set; }
}
