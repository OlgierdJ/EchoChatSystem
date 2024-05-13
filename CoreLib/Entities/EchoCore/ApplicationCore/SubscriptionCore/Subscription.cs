using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore
{

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




    public class Subscription : BaseEntity<ulong>
    {
        public ulong BillingInformationId { get; set; } //ownerid
        public ulong SubscriptionPlanId { get; set; } //specific plan
        public uint CurrencyId { get; set; }

        /// <summary>
        /// Time of when the subscription was agreed to
        /// </summary>
        public DateTime TimeSubscribed { get; set; }
        /// <summary>
        /// Time of when either the user or echo cancelled the subscription.
        /// </summary>
        public DateTime? TimeCancelled { get; set; }
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
        public bool IsRecurring { get; set; }

        /// <summary>
        /// the user has to agree to allowing echo to stop the subscription upon user violations or subscription changes.
        /// </summary>
        public bool AgreeToEchosWithdrawalRight { get; set; }

        public BillingInformation BillingInformation { get; set; } //owner
        public SubscriptionPlan SubscriptionPlan { get; set; } //type of subscription
        public AcceptedCurrency Currency { get; set; } //Currency which is charged from payment method
        /// <summary>
        /// list of transactions made from the specific subscription (effectively tracking payments, active periods of the subscription aswell if it has been paused, refunded and such)
        /// </summary>
        public ICollection<SubscriptionTransaction>? SubcriptionTransactions { get; set; }
    }




}
