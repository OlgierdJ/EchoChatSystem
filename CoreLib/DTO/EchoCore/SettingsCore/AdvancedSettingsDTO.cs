namespace CoreLib.DTO.EchoCore.SettingsCore
{
    public class AdvancedSettingsDTO
    {
        public ulong Id { get; set; }
        public bool DeveloperMode { get; set; }
        public bool UseHardwareAccelerationToMakeEchoSmoother { get; set; }
        public bool AutoNavigateServerHome { get; set; }
    }
}
