using Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;
using Microsoft.EntityFrameworkCore;
using Echo.Domain.EntityFrameworkCore.EFCORE.Configurations.AccountCore;
using Echo.Domain.EntityFrameworkCore.EFCORE.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Echo.Domain.EntityFrameworkCore.DomainEvents;

namespace Echo.Domain.EntityFrameworkCore.EFCORE;

public class EchoDbContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;

    public EchoDbContext(DbContextOptions<EchoDbContext> options, IServiceProvider serviceProvider) : base(options)
    {
        this._serviceProvider = serviceProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.AddInterceptors(new PublishDomainEventsInterceptor(_serviceProvider.GetRequiredService<IDomainEventService>()));
        //optionsBuilder.AddInterceptors(new PublishTransactionDomainEventsInterceptor(_serviceProvider.GetRequiredService<IDomainEventService>()));
        base.OnConfiguring(optionsBuilder);
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
