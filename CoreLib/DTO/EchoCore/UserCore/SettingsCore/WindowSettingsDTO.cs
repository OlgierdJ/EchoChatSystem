namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class WindowSettingsDTO
    {
        public ulong Id { get; set; }
        public bool OpenEchoOnPCStartup { get; set; }
        public bool StartMinimized { get; set; }
        public bool MinimizeOnClose { get; set; }
    }
}
