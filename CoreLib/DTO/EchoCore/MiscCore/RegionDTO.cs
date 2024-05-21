namespace CoreLib.DTO.EchoCore.MiscCore
{
    public interface IRegion
    {
        uint Id { get; set; }
        string Name { get; set; }
        public string Icon { get; set; }
        string RegionServerURL { get; set; }
    }

    public class RegionDTO : IRegion
    //displayed alphabetically sorted
    {
        public uint Id { get; set; }
        public string Name { get; set; } //server region name or automatic.
        public string Icon { get; set; } //server region name or automatic.
        public string RegionServerURL { get; set; } //hubserver
    }
}