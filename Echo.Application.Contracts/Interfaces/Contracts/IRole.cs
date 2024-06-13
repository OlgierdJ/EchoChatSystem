namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IRole<TRecipient, TPermission>
{
    public string Name { get; set; }

    public ICollection<TRecipient>? Recipients { get; set; }
    public ICollection<TPermission>? Permissions { get; set; }
}
