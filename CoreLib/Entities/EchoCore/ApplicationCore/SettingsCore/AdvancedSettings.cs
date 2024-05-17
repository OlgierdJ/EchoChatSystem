using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class AdvancedSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool DeveloperMode { get; set; }
        public bool UseHardwareAccelerationToMakeEchoSmoother { get; set; }
        public bool AutoNavigateServerHome { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
