global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
using Echo.Application.Clients.EchoChatAPIServiceClients;
using Echo.Application.Clients.EchoChatPushNotificationServiceClients;
using Echo.Chat.Web;
using Echo.Chat.Web.Client.Provider;
using Echo.Chat.Web.Components;
using Echo.Chat.Web.Models;
using EchoWebapp.Client.Provider;
using EchoWebapp.Components;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using OpenIddict.Client;
using Quartz;
using System.Net.Http.Headers;
using System.Text;
using Yarp.ReverseProxy.Transforms;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static OpenIddict.Client.AspNetCore.OpenIddictClientAspNetCoreConstants;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

var connectionString = builder.Configuration.GetValue<string>("EchoDBConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var instanceScopes = builder.Configuration.GetValue<string>("InstanceScopes")
    ?? throw new InvalidOperationException("section 'ServerInstanceName' not found.");

var serverInstanceName = builder.Configuration.GetValue<string>("ServerInstanceName")
    ?? throw new InvalidOperationException("section 'ServerInstanceName' not found.");

var clientSecret = builder.Configuration.GetValue<string>("ClientSecret")
    ?? throw new InvalidOperationException("section 'ClientSecret' not found.");

var authService = builder.Configuration.GetValue<string>("AuthService")
    ?? throw new InvalidOperationException("section 'AuthService' not found.");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient<EchoChatApiServiceClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new($"https+http://{authService}");
});

builder.Services.AddMudServices();

builder.Services.AddCascadingAuthenticationState();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configure the context to use sqlite.
    options.UseSqlServer(connectionString);

    // Register the entity sets needed by OpenIddict.
    // Note: use the generic overload if you need
    // to replace the default OpenIddict entities.
    options.UseOpenIddict();
});

//// Configure the antiforgery stack to allow extracting
//// antiforgery tokens from the X-XSRF-TOKEN header.
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "__Host-X-XSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(50);
    options.SlidingExpiration = false;
});

// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations from the database) at regular intervals.
builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);


builder.Services.AddOpenIddict()

            // Register the OpenIddict core components.
            .AddCore(options =>
            {
                // Configure OpenIddict to use the Entity Framework Core stores and models.
                // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
                options.UseEntityFrameworkCore()
                       .UseDbContext<ApplicationDbContext>();

                // Developers who prefer using MongoDB can remove the previous lines
                // and configure OpenIddict to use the specified MongoDB database:
                // options.UseMongoDb()
                //        .UseDatabase(new MongoClient().GetDatabase("openiddict"));

                // Enable Quartz.NET integration.
                options.UseQuartz();
            })

            // Register the OpenIddict client components.
            .AddClient(options =>
            {
                // Note: this sample uses the code flow, but you can enable the other flows if necessary.
                options.AllowAuthorizationCodeFlow();

                // Register the signing and encryption credentials used to protect
                // sensitive data like the state tokens produced by OpenIddict.
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableStatusCodePagesIntegration()
                       .EnableRedirectionEndpointPassthrough()
                       .EnablePostLogoutRedirectionEndpointPassthrough();

                // Register the System.Net.Http integration and use the identity of the current
                // assembly as a more specific user agent, which can be useful when dealing with
                // providers that use the user agent as a way to throttle requests (e.g Reddit).
                options.UseSystemNetHttp()
                       .SetProductInformation(typeof(Program).Assembly);
                // Add a client registration matching the client application definition in the server project.
                var oidcClientReg =
                    new OpenIddictClientRegistration
                    {
                        Issuer = new Uri(authService, UriKind.Absolute),

                        ClientId = serverInstanceName,
                        ClientSecret = clientSecret,
                        //Scopes = { Scopes.Profile, "api1" },
                        Scopes = { Scopes.Profile, },

                        // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
                        // URI per provider, unless all the registered providers support returning a special "iss"
                        // parameter containing their URL as part of authorization responses. For more information,
                        // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
                        RedirectUri = new Uri("callback/login/local", UriKind.Relative),
                        PostLogoutRedirectUri = new Uri("callback/logout/local", UriKind.Relative)
                    };
                foreach (var item in instanceScopes.Split(","))
                {
                    oidcClientReg.Scopes.Add(item.Trim());
                }
                options.AddRegistration(oidcClientReg);
            });


//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.SaveToken = true;
//    x.TokenValidationParameters = new()
//    {
//        ValidIssuer = config["JwtSettings:Issuer"],
//        ValidAudience = config["JwtSettings:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey
//        (Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        RequireExpirationTime = true,
//        ValidateIssuerSigningKey = true,
//        ValidateLifetime = true
//    };
//});


//builder.Services.AddAuthorizationCore();

// Create an authorization policy used by YARP when forwarding requests
// from the WASM application to the Dantooine.Api resource server.
builder.Services.AddAuthorization(options => options.AddPolicy("CookieAuthenticationPolicy", builder =>
{
    builder.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
    builder.RequireAuthenticatedUser();
}));

builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
            .AddTransforms(builder => builder.AddRequestTransform(async context =>
            {
                // Attach the access token retrieved from the authentication cookie.
                //
                // Note: in a real world application, the expiration date of the access token
                // should be checked before sending a request to avoid getting a 401 response.
                // Once expired, a new access token could be retrieved using the OAuth 2.0
                // refresh token grant (which could be done transparently).
                var token = await context.HttpContext.GetTokenAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    tokenName: Tokens.BackchannelAccessToken);

                context.ProxyRequest.Headers.Authorization = new AuthenticationHeaderValue(Schemes.Bearer, token);
            }));

// Register the worker responsible for creating the database used to store tokens.
// Note: in a real world application, this step should be part of a setup script.
builder.Services.AddHostedService<Worker>();

builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddSingleton<EchoAPI>();
builder.Services.AddScoped<AccountIdContainer>();
builder.Services.AddScoped<EchoChatPushNotificationServiceClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IUserContainer, UserContainer>();
//builder.Services.AddSingleton<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
//builder.Services.AddScoped<AuthenticationService>();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Echo.Chat.Web.Client._Imports).Assembly);

app.MapDefaultEndpoints();

app.Run();
