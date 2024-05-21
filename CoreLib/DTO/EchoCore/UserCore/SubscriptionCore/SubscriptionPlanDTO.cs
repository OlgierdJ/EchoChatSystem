namespace CoreLib.DTO.EchoCore.UserCore.SubscriptionCore
{
    public interface ISubscriptionPlan
    {
        double CostEUR { get; set; }
        ulong Id { get; set; }
        string Name { get; set; }
        string PaymentPlan { get; set; }
        string Type { get; set; }
    }

    public class SubscriptionPlanDTO : ISubscriptionPlan
    {
        public ulong Id { get; set; }
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
    }
}
