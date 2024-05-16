using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore
{
    public class SubscriptionPlanActivePeriod : BaseEntity<ulong>
    {
        /// <summary>
        /// specific plan
        /// </summary>
        public ulong SubscriptionPlanId { get; set; }
        /// <summary>
        /// start time of the availability of the plan
        /// </summary>
        public DateTime TimeStarts { get; set; }
        /// <summary>
        /// End date of the availability of the plan
        /// null means neverending unless disabled.
        /// </summary>
        public DateTime? TimeStops { get; set; }
        /// <summary>
        /// Flag disabling the opportunity to purchase the plan
        /// </summary>
        public bool IsTemporarilyDisabled { get; set; }
        /// <summary>
        /// sale percentage during sale period used for calculating the users price for their specific subscription instance.
        /// </summary>
        public double? PercentageSale { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
