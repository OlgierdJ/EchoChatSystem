using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ReportCore.CustomStatus
{
    public class ReportedCustomStatus : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string CustomMessage { get; set; }
        public Account Account { get; set; }
        public CustomStatusReport Report { get; set; }
    }
}
