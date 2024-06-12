namespace CoreLib.Interfaces.Contracts;

public interface IMutable<TMute>
{
    public ICollection<TMute>? Muters { get; set; }
}
