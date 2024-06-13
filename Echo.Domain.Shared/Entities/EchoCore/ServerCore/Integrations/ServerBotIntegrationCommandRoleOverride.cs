using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotIntegrationCommandRoleOverride : BaseEntity<ulong>
{
    public ulong ServerBotIntegrationId { get; set; }
    public ulong ServerBotCommandId { get; set; }
    public ulong ServerRoleId { get; set; }
    /// <summary>
    /// whether or not a certain role is permitted to use the bot. 
    /// (if bot is not permitted by everyone but is permitted by certain role then only if the user has the role they are allowed to use the bot)
    /// THIS RESTRICTION OVERRIDES THE GLOBAL ROLE RESTRICTIONS
    /// THIS RESTRICTION SHOULD BE OVERRIDDEN BY MEMBER RESTRICTIONS
    /// </summary>
    public bool Permitted { get; set; }
    public ServerBotIntegration ServerBotIntegration { get; set; }
    public ServerBotCommand ServerBotCommand { get; set; }
    public ServerRole ServerRole { get; set; }
}