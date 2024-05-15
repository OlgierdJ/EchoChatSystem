using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.ModerationCore
{
    public class ServerAuditLogConfiguration : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }

        public DateTime TimeLogged { get; set; }
        public string Action { get; set; }

        public Account Account { get; set; }
        public ServerConfiguration Server { get; set; }

    }
}