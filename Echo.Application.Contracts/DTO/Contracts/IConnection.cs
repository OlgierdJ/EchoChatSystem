namespace Echo.Application.Contracts.DTO.Contracts;

public interface IConnection
{
    //uint Id { get; set; } inherit from iidentified or ientity instead
    string PlatformIcon { get; set; }
    string PlatformName { get; set; }
}
