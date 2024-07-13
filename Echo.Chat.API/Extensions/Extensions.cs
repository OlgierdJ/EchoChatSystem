using System.Text.Json.Serialization;
using DomainCoreApi.Handlers;
using Echo.Domain.EntityFrameworkCore.EFCORE.Interceptors;
using Echo.Domain.EntityFrameworkCore.EFCORE;
using Echo.SystemOrchestrator.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using DomainCoreApi.Swagger;
using Echo.Chat.API.Services;
using Echo.Domain.EntityFrameworkCore.DomainEvents;
using Echo.Domain.EntityFrameworkCore.Services;
using Echo.Domain.Shared.Handlers;
using Echo.Domain.Shared.Interfaces.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Echo.Domain.Shared.Constants;
using Echo.Domain.Shared.Interfaces.Handlers;
//using eShop.Basket.API.IntegrationEvents.EventHandling;
//using eShop.Basket.API.IntegrationEvents.EventHandling.Events;

namespace Echo.Chat.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        //var connectionString = builder.Configuration.GetConnectionString("DbConnection")
        //    ?? throw new InvalidOperationException($"Connection string '{"DbConnection"}' not found.");
        builder.AddSqlServerDbContext<EchoDbContext>("domaindb", configureDbContextOptions: dbContextOptions =>
        {
            dbContextOptions.AddInterceptors();
        });
        //builder.Services.AddDbContext<EchoDbContext>((sp, options) =>
        //options.UseSqlServer(connectionString).AddInterceptors(
        //            sp.GetRequiredService<PublishDomainEventsInterceptor>(),
        //            sp.GetRequiredService<PublishTransactionDomainEventsInterceptor>()
        //));

        var services = builder.Services;
        builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.Position));
        //builder.AddDefaultAuthentication();
        builder.AddOIDCAuthentication();

        //builder.AddRedisClient("redis");

        //builder.Services.AddSingleton<IBasketRepository, RedisBasketRepository>();

        builder.AddRabbitMqEventBus("eventbus")
            .AddEventBusSubscriptions();
        //.ConfigureJsonOptions(options => options.TypeInfoResolverChain.Add(IntegrationEventContext.Default));

        //builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        //builder.Services.AddScoped<IPublisher, PushNotificationPublisher>();
        //builder.Services.AddScoped<IDomainEventService, DomainEventService>();
        //builder.Services.AddScoped<PublishDomainEventsInterceptor>();
        //builder.Services.AddScoped<PublishTransactionDomainEventsInterceptor>();

        //builder.Services.AddTransient<ITokenHandler, DomainCoreApi.Handlers.TokenHandler>();
        //builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
        //builder.Services.AddTransient(typeof(IChatService), typeof(ChatService));
        //builder.Services.AddTransient(typeof(IUserGroupService), typeof(UserGroupService));

        //builder.Services.AddTransient(typeof(IPasswordHandler), typeof(Passwordhandler));

        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy(EchoDomainConstants.PublicCorsPolicy, builder =>
        //    {
        //        builder.AllowAnyOrigin()
        //        .AllowAnyHeader()
        //        .AllowAnyMethod();
        //        //.SetIsOriginAllowed((host) => true);
        //    });
        //});

        //builder.Services.AddControllers().AddJsonOptions(opts =>
        //{
        //    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        //});

        //builder.Services.AddSignalR().AddJsonProtocol(opts => opts.PayloadSerializerOptions = new JsonSerializerOptions()
        //{
        //    ReferenceHandler = ReferenceHandler.Preserve,

        //});

        //builder.Services.AddAutoMapper(opts =>
        //{
        //    opts.AddProfile<EchoCoreCommonMappings>();
        //});
    }

    private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
        //eventBus.AddSubscription<GracePeriodConfirmedIntegrationEvent, GracePeriodConfirmedIntegrationEventHandler>();
        //eventBus.AddSubscription<OrderStockConfirmedIntegrationEvent, OrderStockConfirmedIntegrationEventHandler>();
        //eventBus.AddSubscription<OrderStockRejectedIntegrationEvent, OrderStockRejectedIntegrationEventHandler>();
        //eventBus.AddSubscription<OrderPaymentFailedIntegrationEvent, OrderPaymentFailedIntegrationEventHandler>();
        //eventBus.AddSubscription<OrderPaymentSucceededIntegrationEvent, OrderPaymentSucceededIntegrationEventHandler>();
    }
}

//[JsonSerializable(typeof(OrderStartedIntegrationEvent))]
//partial class IntegrationEventContext : JsonSerializerContext
//{

//}
