namespace Echo.Application.Contracts.DTO.Contracts;

public interface IKeybind
{
    string? Action { get; set; }
    string? Description { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    string Name { get; set; }
}
