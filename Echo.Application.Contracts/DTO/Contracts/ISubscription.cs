using Echo.Application.Contracts.DTO.EchoCore.MiscCore;
using Echo.Application.Contracts.DTO.EchoCore.UserCore.SubscriptionCore;

namespace Echo.Application.Contracts.DTO.Contracts;

public interface ISubscription
{
    CurrencyDTO Currency { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    bool IsRecurring { get; set; }
    double MyCostEUR { get; set; }
    SubscriptionPlanDTO SubscriptionPlan { get; set; }
    DateTime? TimeDeadline { get; set; }
}
