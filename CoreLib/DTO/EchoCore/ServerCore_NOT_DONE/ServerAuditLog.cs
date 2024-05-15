using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerAuditLog //: BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }
        public DateTime TimeLogged { get; set; }
        public string Action { get; set; }

        public Account Account { get; set; }
        public Server Server { get; set; }

    }
}