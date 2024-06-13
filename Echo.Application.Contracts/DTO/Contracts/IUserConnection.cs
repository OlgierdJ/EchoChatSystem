using Echo.Application.Contracts.DTO.EchoCore.MiscCore;

namespace Echo.Application.Contracts.DTO.Contracts;

public interface IUserConnection
{
    bool DisplayOnProfile { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    string Name { get; set; }
    ConnectionDTO Type { get; set; }
}
