using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;

namespace CoreLib.Entities.EchoCore.ServerCore.Integrations
{
    public class ServerBotIntegration : BaseEntity<ulong>
    {
        public ulong ServerBotId { get; set; }
        public ulong ServerId { get; set; }
        public ulong IntegratorAccountId { get; set; }

        public bool GrantEveryoneRole { get; set; }
        public bool PermitAllChannels { get; set; }

        public ICollection<ServerBotIntegrationCommandTextChannelOverride>? CommandTextChannelOverrides { get; set; }
        public ICollection<ServerBotIntegrationCommandVoiceChannelOverride>? CommandVoiceChannelOverrides { get; set; }
        public ICollection<ServerBotIntegrationCommandRoleOverride>? CommandRoleOverrides { get; set; }
        public ICollection<ServerBotIntegrationCommandMemberOverride>? CommandMemberOverrides { get; set; }
        public ICollection<ServerBotIntegrationRoleRestriction>? RoleRestrictions { get; set; }
        public ICollection<ServerBotIntegrationMemberRestriction>? MemberRestrictions { get; set; }
        public ICollection<ServerBotIntegrationTextChannel>? TextChannels { get; set; }
        public ICollection<ServerBotIntegrationVoiceChannel>? VoiceChannels { get; set; }
        public ICollection<ServerBotIntegrationTextChannelWebhook>? TextChannelWebhooks { get; set; }
        public ICollection<ServerBotIntegrationVoiceChannelWebhook>? VoiceChannelWebhooks { get; set; }
        public ServerBot ServerBot { get; set; } //Bot application containing both bot, commands, bot account and profile / serverprofiles
        public Server Server { get; set; }
        public Account IntegratorAccount { get; set; }
    }
}