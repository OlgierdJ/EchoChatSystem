namespace CoreLib.DTO.Contracts
{
    public interface IPinboard<TPinnedMessage>
    {
        //ulong Id { get; set; } inherit from iidentified or ientity instead
        ICollection<TPinnedMessage>? PinnedMessages { get; set; }
    }
}
