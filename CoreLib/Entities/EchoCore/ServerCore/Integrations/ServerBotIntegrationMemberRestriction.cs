using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotIntegrationMemberRestriction : BaseEntity<ulong>
{
    public ulong ServerBotIntegrationId { get; set; }
    public ulong ServerProfileId { get; set; }
    /// <summary>
    /// whether or not a certain member regardless of role is permitted to use the bot. 
    /// THIS OVERRIDES ROLE RESTRICTIONS MEANING YES YOU CAN BAN A CERTAIN ADMIN FROM USING A BOT IF THEY ARE ANNOYING
    /// </summary>
    public bool Permitted { get; set; }
    public ServerBotIntegration ServerBotIntegration { get; set; }
    public ServerProfile ServerRole { get; set; }
}