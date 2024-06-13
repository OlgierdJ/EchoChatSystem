using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotIntegrationVoiceChannelWebhook : BaseEntity<ulong>
{
    public ulong ServerBotIntegrationId { get; set; }
    public ulong VoiceChannelId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string WebhookEndpointURL { get; set; }
    //https://discord.com/api/webhooks/1214486375198363708/bO7ioVXfzPFnxfI2Gitcy9wLwLU7oD_5p5BjljQeVX_aTALnhtHQItHAtXCQtNqH4wDg
    //https://discord.com/api/webhooks/1214487431173382194/thdqRsUDC9Z_dgIGG76JvLaCgY6ne0RAQkig6vxBnwViBdjZgN5s4CSiHDtdxWiStBfQ
    public ServerBotIntegration ServerBotIntegration { get; set; }
    public ServerVoiceChannel VoiceChannel { get; set; }
}