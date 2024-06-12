namespace CoreLib.Interfaces.Contracts;

public interface IMediaLink<TOwner, TOwnerId>
{
    public TOwnerId OwnerId { get; set; }
    public string URL { get; set; }
    public string? IconURL { get; set; }
    public uint Importance { get; set; }
    public TOwner Owner { get; set; }
}