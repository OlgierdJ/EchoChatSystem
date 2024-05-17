using CoreLib.Entities.EchoCore.ServerCore.Integrations;
using DomainCoreApi.EFCORE.Configurations.AccountCore;
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
            modelBuilder.Ignore<ServerBotIntegrationCommandMemberOverride>();
            modelBuilder.Ignore<ServerBotIntegrationCommandRoleOverride>();
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
        }
    }
}
