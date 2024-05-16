namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class RegionDTO //displayed alphabetically sorted
    {
        public uint Id { get; set; }
        public string Name { get; set; } //server region name or automatic.
        public string RegionServerURL { get; set; } //hubserver
    }
}