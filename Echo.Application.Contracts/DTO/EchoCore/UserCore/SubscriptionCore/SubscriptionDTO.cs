using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.DTO.EchoCore.MiscCore;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SubscriptionCore;


/*
 * ******************************************* 
 * Subscription Plan
 * User should be able to view different types of subscriptions plans allowing them to subscribe to them.
 * 
 * The subscription plan should have a cost, which can be changed by sales and such (reflected in the users subscription to the plan).
 * Furthermore the subscription plan should have a variable which decides how often the cost is charged.
 * 
 * Also the plan should have a type which determines if the subscription should be able to be renewed, changed to another plan or stopped, after first period
 * 
 * The subscription plan should specify which role the user is granted from subscribing.
 * 
 * ********************************************
 * The users subscription should allow the user to subscribe to a plan,
 * specifying user variables such as allowing recurring subscription period, 
 * their specific cost, agreement to echos withdrawal right, their specific currency charged.
 * It should also track start date of subscribing date.
 * Furthermore the subscription should allow echo to specify a deadline after which the subscription cannot be renewed by charge.
 * 
 * 
 * 
 * allow certain permissions by applying "role" to the user.
 */




public class SubscriptionDTO : ISubscription
//: BaseEntity<ulong>
{
    public ulong Id { get; set; } //owner id
    //public ulong PlanId { get; set; } //specific plan
    //public uint CurrencyId { get; set; }
    /// <summary>
    /// time after which the transaction cannot autorenew used for stopping specific variable plan after period or tracking renegotiation period (changing the plan or allowing it to continue)
    /// </summary>
    public DateTime? TimeDeadline { get; set; }

    /// <summary>
    /// variable (depending on sales and type) cost charged when user subscribed, used for continuing the same price upon further charges if subscription is recurring.
    /// </summary>
    public double MyCostEUR { get; set; }

    /// <summary>
    /// whether or not echo should charge to extend the subscription period when the original subscription runs out.
    /// </summary>
    public bool IsRecurring { get; set; } //if true display recurring else display pending cancellation
    public SubscriptionPlanDTO SubscriptionPlan { get; set; } //type of subscription
    public CurrencyDTO Currency { get; set; } //Currency which is charged from payment method

}
