namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public interface IWindowSettings
    {
        ulong Id { get; set; }
        bool MinimizeOnClose { get; set; }
        bool OpenEchoOnPCStartup { get; set; }
        bool StartMinimized { get; set; }
    }

    public class WindowSettingsDTO : IWindowSettings
    {
        public ulong Id { get; set; }
        public bool OpenEchoOnPCStartup { get; set; }
        public bool StartMinimized { get; set; }
        public bool MinimizeOnClose { get; set; }
    }
}
