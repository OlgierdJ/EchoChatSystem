namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountAccountVolume
    {
        public ulong OwnerId { get; set; }
        public ulong AffectedId { get; set; }
        public byte Volume { get; set; }

        public Account Owner { get; set; }
        public Account Affected { get; set; }
    }
}
