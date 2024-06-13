namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface ICategorisable<TCategory, TCategoryId>
{
    public TCategoryId? CategoryId { get; set; }
    public TCategory? Category { get; set; }
}