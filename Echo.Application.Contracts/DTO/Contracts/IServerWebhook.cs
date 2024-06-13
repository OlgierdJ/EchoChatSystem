namespace Echo.Application.Contracts.DTO.Contracts;

public interface IServerWebhook
{
    ulong Id { get; set; }
    string ImageUrl { get; set; }
    string Name { get; set; }
    string WebhookEndpointURL { get; set; }
}
