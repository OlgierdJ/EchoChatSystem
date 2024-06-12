using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.ServerCore;


public class ServerWebhookDTO : IServerWebhook
{
    public ulong Id { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string WebhookEndpointURL { get; set; }
}
