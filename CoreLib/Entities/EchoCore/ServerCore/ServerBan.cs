using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerBan : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string Reason { get; set; }
        public DateTime? TimeExpired { get; set; } //null = perma
        public Account Account { get; set; }
    }
}