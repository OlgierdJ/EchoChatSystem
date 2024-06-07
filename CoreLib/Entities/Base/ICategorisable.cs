namespace CoreLib.Entities.Base
{
    public interface ICategorisable<TCategory, TCategoryId>
    {
        public TCategoryId? CategoryId { get; set; }
        public TCategory? Category { get; set; }
    }
}