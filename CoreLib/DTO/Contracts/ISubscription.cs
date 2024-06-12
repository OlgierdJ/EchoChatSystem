using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.UserCore.SubscriptionCore;

namespace CoreLib.DTO.Contracts;

public interface ISubscription
{
    CurrencyDTO Currency { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    bool IsRecurring { get; set; }
    double MyCostEUR { get; set; }
    SubscriptionPlanDTO SubscriptionPlan { get; set; }
    DateTime? TimeDeadline { get; set; }
}
