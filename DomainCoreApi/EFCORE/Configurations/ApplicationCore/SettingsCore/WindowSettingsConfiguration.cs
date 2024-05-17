using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace DomainCoreApi.EFCORE.Configurations.ApplicationCore.Settings
{
    public class WindowSettingsConfiguration : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool OpenEchoOnPCStartup { get; set; }
        public bool StartMinimized { get; set; }
        public bool MinimizeOnClose { get; set; }

        public AccountSettings AccountSettings { get; set; }
    }
}
