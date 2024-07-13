using Echo.Domain.EntityFrameworkCore.EFCORE;
using Echo.Domain.Shared.Constants;
using DomainPushNotificationApi.Services;
using Echo.Domain.Shared.MapperProfiles;
using Echo.Application.Contracts.Interfaces.Providers;
using Echo.Domain.Shared.EchoChatApiServiceServerClients;
//using eShop.Basket.API.IntegrationEvents.EventHandling;
//using eShop.Basket.API.IntegrationEvents.EventHandling.Events;

namespace Echo.Chat.PushNotification.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        //var connectionString = builder.Configuration.GetConnectionString("DbConnection")
        //    ?? throw new InvalidOperationException($"Connection string '{"DbConnection"}' not found.");
        builder.AddSqlServerDbContext<EchoDbContext>("domaindb", configureDbContextOptions: dbContextOptions =>
        {
            //dbContextOptions.AddInterceptors();
        });
        //builder.Services.AddDbContext<EchoDbContext>((sp, options) =>
        //options.UseSqlServer(connectionString).AddInterceptors(
        //            sp.GetRequiredService<PublishDomainEventsInterceptor>(),
        //            sp.GetRequiredService<PublishTransactionDomainEventsInterceptor>()
        //));

        //var services = builder.Services;
        //builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.Position));
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

        //var authService = builder.Configuration.GetValue<string>(EchoDomainConstants.AuthService)
        //    ?? throw new InvalidOperationException($"section '{EchoDomainConstants.AuthService}' not found.");

        //var apiService = builder.Configuration.GetValue<string>(EchoDomainConstants.ChatApiService)
        //    ?? throw new InvalidOperationException($"section '{EchoDomainConstants.ChatApiService}' not found.");

        //builder.Services.AddHttpClient("DomainClient", e => 
        //{
        //    e.BaseAddress = new Uri("https://localhost:7269/api");
        //    e.DefaultRequestHeaders.Accept.Clear();
        //    e.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //});

        builder.Services.AddHttpClient<EchoChatApiServiceServerClient>(client =>
        {
            // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
            // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
            client.BaseAddress = new("https+http://chat-api");
        });

        builder.Services.AddAutoMapper(opts =>
        {
            opts.AddProfile<EchoCoreCommonMappings>();
        });
        builder.Services.AddSingleton<ITokenProvider, TokenStore>();
        builder.Services.AddSingleton<PushNotificationClientConnectionStore>();
        builder.Services.AddSingleton<PushNotificationService>();
        builder.Services.AddSingleton<DomainNotificationClientService>();


        builder.Services.AddHostedService<StartupBackgroundService>();

        builder.Services.AddSignalR().AddStackExchangeRedis("cache", opts =>
        {
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(EchoDomainConstants.PublicCorsPolicy, builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
                //.SetIsOriginAllowed((host) => true);
            });
        });

        //builder.Services.AddTransient(typeof(IPasswordHandler), typeof(Passwordhandler));

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
