using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.ServerCore;


public class ServerMinimalDTO : IServerMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string ImageIconURL { get; set; }
}
