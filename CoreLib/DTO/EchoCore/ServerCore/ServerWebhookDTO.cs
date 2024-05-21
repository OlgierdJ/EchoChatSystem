namespace CoreLib.DTO.EchoCore.ServerCore
{
    public interface IServerWebhook
    {
        ulong Id { get; set; }
        string ImageUrl { get; set; }
        string Name { get; set; }
        string WebhookEndpointURL { get; set; }
    }

    public class ServerWebhookDTO : IServerWebhook
    {
        public ulong Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string WebhookEndpointURL { get; set; }
    }
}
