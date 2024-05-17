namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class ConnectionDTO //this only represents the platform.
    {
        public uint Id { get; set; } //connectiontype id
        public string PlatformName { get; set; } //facebook, league of legends, steam, etc.
        public string PlatformIcon { get; set; } //facebook, league of legends, steam, etc.

    }

}
