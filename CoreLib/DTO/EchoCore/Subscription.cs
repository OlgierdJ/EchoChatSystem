using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore
{
    public class SubscriptionPlan //: BaseEntity<long>
    {
        //is it recurring or onetime
        //paid? free?
        //what is it unlocking
        //tier of unlocked content?
       
        public string Name { get; set; } //Sonar, Sonar+, Sonar Pro, etc..
        public double CostEUR { get; set; } //10.99eur, 19.99eur, etc..
        public string PaymentPlan {  get; set; } // Daily (every day), Weekly (every week), Biweekly (every 2 weeks), Monthly (every month), Bimestrial (every 2 months), Quarterly (every 3 months), Semestral (every 6 months), Annualy (every year), Biennial (every other year), Triennial (every third year)
        public string Type { get; set; } //Trial, Limited, Full, etc..

        public ICollection<Subscription>? Subscriptions { get; set; }
    }
    public class Subscription : BaseEntity<long>
    {
        public ulong AccountId { get; set; }
        public ulong SubscriptionPlanId { get; set; }

        public double MyCostEUR { get; set; } 
        public bool IsRecurring { get; set; } 

        public SubscriptionStatus Status { get; set; } //Paused, Active, Suspended,
        public DateTime StartTime { get; set; } //Date of when the subscription starts
        public DateTime ExpirationTime { get; set; } //Time of when the subscription ends
        public DateTime? TimePaused { get; set; } //time the subscription was paused used for calculating remaining subscription upon starting it again
       
        public Account Account { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
    }

    public enum SubscriptionStatus
    {
        Paused=0, Active=1, Suspended=2, Failed=3
    }
}
