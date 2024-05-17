namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerWebhookDTO
    {
        public ulong Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string WebhookEndpointURL { get; set; }
    }
}
