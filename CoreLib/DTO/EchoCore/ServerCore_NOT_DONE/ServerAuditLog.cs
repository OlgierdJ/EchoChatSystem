using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerAuditLog 
    {
        public ulong Id { get; set; }
        public DateTime TimeLogged { get; set; }
        public string Action { get; set; }

    }
}