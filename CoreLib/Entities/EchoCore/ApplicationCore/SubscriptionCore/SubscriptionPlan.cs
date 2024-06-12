using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;

public class SubscriptionPlan : BaseEntity<ulong>
{
    //is it recurring or onetime
    //paid? free?
    //what is it unlocking
    //tier of unlocked content?
    public ulong RoleId { get; set; }
    /// <summary>
    /// Sonar, Sonar+, Sonar Pro, etc..
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 10.99eur, 19.99eur, etc..
    /// will be converted to specified valuta in the variable subscription
    /// </summary>
    public double CostEUR { get; set; }
    /// <summary>
    /// Daily (every day), Weekly (every week), Biweekly (every 2 weeks), Monthly (every month), Bimestrial (every 2 months), Quarterly (every 3 months), Semestral (every 6 months), Annualy (every year), Biennial (every other year), Triennial (every third year)
    /// </summary>
    public string PaymentPlan { get; set; }
    /// <summary>
    /// Trial, Limited, Full, etc..
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// subscriptions created based on plan.
    /// </summary>
    public ICollection<Subscription>? Subscriptions { get; set; }
    /// <summary>
    /// used for specifying and retrieving the subscriptions which the user can buy during the specific period of retrieval.
    /// </summary>
    public ICollection<SubscriptionPlanActivePeriod>? ActivePeriods { get; set; }
    /// <summary>
    /// given role upon subscribing to the plan.
    /// </summary>
    public Role Role { get; set; }
}
