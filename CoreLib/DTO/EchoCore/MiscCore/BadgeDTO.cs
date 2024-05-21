namespace CoreLib.DTO.EchoCore.MiscCore
{
    public interface IBadge
    {
        string Description { get; set; }
        string IconURL { get; set; }
        ulong Id { get; set; }
        int OrderingWeight { get; set; }
    }

    public class BadgeDTO : IBadge
    {
        public ulong Id { get; set; }
        public int OrderingWeight { get; set; }
        public string Description { get; set; }
        public string IconURL { get; set; }
    }
}