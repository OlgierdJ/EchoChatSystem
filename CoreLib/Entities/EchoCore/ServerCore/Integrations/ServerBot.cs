using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations;

public class ServerBot : BaseEntity<ulong>
{
    public ulong BotActorAccountId { get; set; } //account that is used by the bot for standard echo interactions
    public ulong? SupportServerId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? TimeAdded { get; set; }
    public ICollection<ServerBotCommand>? Commands { get; set; }
    public ICollection<ServerBotMediaLink>? Links { get; set; }
    public ICollection<ServerBotImage>? Images { get; set; }
    public ICollection<Language>? SupportedLanguages { get; set; }
    public Account BotActorAccount { get; set; }
    public Server? SupportServer { get; set; }
}
