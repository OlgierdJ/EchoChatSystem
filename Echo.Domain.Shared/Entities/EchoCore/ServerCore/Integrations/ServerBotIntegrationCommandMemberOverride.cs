using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotIntegrationCommandMemberOverride : BaseEntity<ulong>
{
    public ulong ServerBotIntegrationId { get; set; }
    public ulong ServerBotCommandId { get; set; }
    public ulong ServerProfileId { get; set; }
    /// <summary>
    /// whether or not a certain member regardless of role is permitted to use the bot. 
    /// THIS RESTRICTION OVERRIDES THE GLOBAL MEMBER RESTRICTIONS
    /// THIS OVERRIDES ROLE RESTRICTIONS MEANING YES YOU CAN BAN A CERTAIN ADMIN FROM USING A BOT IF THEY ARE ANNOYING
    /// </summary>
    public bool Permitted { get; set; }
    public ServerBotIntegration ServerBotIntegration { get; set; }
    public ServerBotCommand ServerBotCommand { get; set; }
    //public ServerProfile ServerRole { get; set; }
}
