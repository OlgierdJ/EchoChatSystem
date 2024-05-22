namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public interface IAdvancedSettings
    {
        bool AutoNavigateServerHome { get; set; }
        bool DeveloperMode { get; set; }
        ulong Id { get; set; }
        bool UseHardwareAccelerationToMakeEchoSmoother { get; set; }
    }

    public class AdvancedSettingsDTO : IAdvancedSettings
    {
        public ulong Id { get; set; }
        public bool DeveloperMode { get; set; }
        public bool UseHardwareAccelerationToMakeEchoSmoother { get; set; }
        public bool AutoNavigateServerHome { get; set; }
    }
}
