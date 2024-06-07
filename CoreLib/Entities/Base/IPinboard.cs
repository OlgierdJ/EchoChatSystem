namespace CoreLib.Entities.Base
{
    public interface IPinboard<TMessagePin>
    {
        public ICollection<TMessagePin>? PinnedMessages { get; set; }
    }
}
