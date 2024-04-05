namespace CoreLib.Entities.Base
{
    public class BaseImage<TId, TOwner, TOwnerId> : BaseEntity<TId>
    {
        public TOwnerId OwnerId { get; set; }
        public string ImageURL { get; set; }
        public uint Importance { get; set; }
        public TOwner Owner { get; set; }
    }
}