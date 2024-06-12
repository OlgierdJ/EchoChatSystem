using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.ServerCore;


public class ServerMinimalDTO : IServerMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string ImageIconURL { get; set; }
}
