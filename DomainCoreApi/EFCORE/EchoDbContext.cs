using CoreLib.Entities.EchoCore;
using CoreLib.Entities.EchoCore.ServerCore;
using CoreLib.Entities.EchoCore.ServerCore.Integrations;
using DomainCoreApi.EFCORE.Configurations.AccountCore;
using DomainCoreApi.EFCORE.Configurations.UserCore;
using Microsoft.EntityFrameworkCore;

namespace DomainCoreApi.EFCORE
{
    public class EchoDbContext : DbContext
    {
        public EchoDbContext(DbContextOptions<EchoDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
            modelBuilder.Ignore<ServerBot>();
            modelBuilder.Ignore<ServerBotCommand>();
            modelBuilder.Ignore<ServerBotCommandParameter>();
            modelBuilder.Ignore<ServerBotCommandParameterValue>();
            modelBuilder.Ignore<ServerBotImage>();
            modelBuilder.Ignore<ServerBotIntegration>();
            modelBuilder.Ignore<ServerBotIntegrationCommandRoleOverride>();
            modelBuilder.Ignore<ServerBotIntegrationCommandMemberOverride>();
            modelBuilder.Ignore<ServerBotIntegrationCommandTextChannelOverride>();
            modelBuilder.Ignore<ServerBotIntegrationCommandVoiceChannelOverride>();
            modelBuilder.Ignore<ServerBotIntegrationMemberRestriction>();
            modelBuilder.Ignore<ServerBotIntegrationRoleRestriction>();
            modelBuilder.Ignore<ServerBotIntegrationTextChannel>();
            modelBuilder.Ignore<ServerBotIntegrationTextChannelWebhook>();
            modelBuilder.Ignore<ServerBotIntegrationVoiceChannel>();
            modelBuilder.Ignore<ServerBotIntegrationVoiceChannelWebhook>();
            modelBuilder.Ignore<ServerBotMediaLink>();
            modelBuilder.Ignore<ServerBotSupportedLanguage>();

            modelBuilder.Ignore<Server>();
            modelBuilder.Ignore<ServerAuditLog>();
            modelBuilder.Ignore<ServerBan>();
            modelBuilder.Ignore<ServerChannelCategory>();
            modelBuilder.Ignore<ServerChannelCategoryMemberSettings>();
            modelBuilder.Ignore<ServerChannelCategoryPermission>();
            modelBuilder.Ignore<ServerChannelPermission>();
            modelBuilder.Ignore<ServerEvent>();
            modelBuilder.Ignore<ServerInvite>();
            modelBuilder.Ignore<ServerMessagePin>();
            modelBuilder.Ignore<ServerProfile>();
            modelBuilder.Ignore<ServerRole>();
            modelBuilder.Ignore<ServerRolePermission>();
            modelBuilder.Ignore<ServerSettings>();
            modelBuilder.Ignore<ServerSoundboardSound>();
            modelBuilder.Ignore<ServerTextChannel>();
            modelBuilder.Ignore<ServerTextChannelAccountMessageTracker>();
            modelBuilder.Ignore<ServerTextChannelMessage>();
            modelBuilder.Ignore<ServerTextChannelPinboard>();
            modelBuilder.Ignore<ServerVoiceChannel>();
            modelBuilder.Ignore<ServerVoiceChannelChannelPermission>();
            modelBuilder.Ignore<ServerWebhook>();
            modelBuilder.Ignore<SubscriptionPlan>();
            modelBuilder.Ignore<Subscription>();
        }
    }
}
