using Microsoft.EntityFrameworkCore;
using Echo.Auth.Data;
using Microsoft.AspNetCore.Identity;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;
using AuthTest;
//using eShop.Basket.API.IntegrationEvents.EventHandling;
//using eShop.Basket.API.IntegrationEvents.EventHandling.Events;

namespace Echo.Auth.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        var connectionString = builder.Configuration.GetConnectionString("DbConnection")
            ?? throw new InvalidOperationException($"Connection string '{"DbConnection"}' not found.");

        //add dbcontext with oidc
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            // Configure the context to use sqlite.
            options.UseSqlServer(connectionString);

            // Register the entity sets needed by OpenIddict.
            // Note: use the generic overload if you need
            // to replace the default OpenIddict entities.
            options.UseOpenIddict();
        });

        // Register the Identity services.
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        //.AddDefaultUI();

        // OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
        // (like pruning orphaned authorizations/tokens from the database) at regular intervals.
        builder.Services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        // Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
        builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        // Register the worker responsible for seeding the database.
        // Note: in a real world application, this step should be part of a setup script.
        builder.Services.AddHostedService<Worker>();

        builder.ConfigureOpenIddict();



        var services = builder.Services;
        //builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(JWTOptions.Position));
        //builder.AddDefaultAuthentication();
        //builder.AddOIDCAuthentication();

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

    private static void ConfigureOpenIddict(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<ApplicationDbContext>();

                // Enable Quartz.NET integration.
                options.UseQuartz();
            })

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                options.RequireProofKeyForCodeExchange();
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("connect/authorize")
                       .SetLogoutEndpointUris("connect/logout")
                       .SetIntrospectionEndpointUris("connect/introspect")
                       .SetTokenEndpointUris("connect/token")
                       .SetUserinfoEndpointUris("connect/userinfo")
                       .SetVerificationEndpointUris("connect/verify");

                // Mark the "email", "profile" and "roles" scopes as supported scopes.
                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                // Note: this sample only uses the authorization code flow but you can enable
                // the other flows if you need to support implicit, password or client credentials.
                options.AllowAuthorizationCodeFlow();

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableLogoutEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .EnableStatusCodePagesIntegration();
            })

            // Register the OpenIddict validation components.
            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });
    }
}

//[JsonSerializable(typeof(OrderStartedIntegrationEvent))]
//partial class IntegrationEventContext : JsonSerializerContext
//{

//}
