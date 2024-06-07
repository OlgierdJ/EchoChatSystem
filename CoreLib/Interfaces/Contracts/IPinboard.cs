namespace CoreLib.Interfaces.Contracts
{
    public interface IPinboard<TMessagePin>
    {
        public ICollection<TMessagePin>? PinnedMessages { get; set; }
    }
}
