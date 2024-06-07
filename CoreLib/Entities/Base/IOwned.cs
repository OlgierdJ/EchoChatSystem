namespace CoreLib.Entities.Base
{
    public interface IOwned<TOwner, TOwnerId>
    {
        public TOwnerId?  OwnerId { get; set; }
        public TOwner?  Owner { get; set; }
    }
}
