namespace CoreLib.Entities.Base
{
    public class BaseMediaLink<TId, TOwner, TOwnerId> : BaseEntity<TId>
    {
        public TOwnerId OwnerId { get; set; }
        public string URL { get; set; }
        public string? IconURL { get; set; }
        public uint Importance { get; set; }
        public TOwner Owner { get; set; }
    }
}