namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IEmote
{
    //ulong Id { get; set; } //inherit from iidentified or ientity or something instead
    string ImageUrl { get; set; }
    string Name { get; set; }
}
