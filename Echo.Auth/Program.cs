using AuthTest;
using Echo.Auth.Data;
using Echo.Auth.Extensions;
using Echo.Domain.EntityFrameworkCore.EFCORE;
using Echo.Domain.Shared.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Quartz;
using Scalar.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
//builder.AddApplicationServices();
builder.Services.AddRazorPages();
// Add services to the container.

builder.Services.AddControllers();
//var connectionString = builder.Configuration.GetConnectionString(EchoDomainConstants.AuthDbConnection)
//    ?? throw new InvalidOperationException($"Connection string '{EchoDomainConstants.AuthDbConnection}' not found.");
builder.AddSqlServerDbContext<ApplicationDbContext>("identitydb", opts1 =>
{
}, opts2 =>
{
    opts2.UseOpenIddict();
});
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    // Configure the context to use sqlite.
//    options.UseSqlServer(connectionString);

//    // Register the entity sets needed by OpenIddict.
//    // Note: use the generic overload if you need
//    // to replace the default OpenIddict entities.
//    options.UseOpenIddict();
//});


//builder.Services.AddIdentityApiEndpoints<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();
// Register the Identity services.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
builder.Services.AddQuartz(options =>
{
    //options.UseMicrosoftDependencyInjectionJobFactory();
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

                // Enable Quartz.NET integration.
                options.UseQuartz();
            })

            //// Register the OpenIddict client components.
            //.AddClient(options =>
            //{
            //    // Note: this sample uses the code flow, but you can enable the other flows if necessary.
            //    options.AllowAuthorizationCodeFlow();

            //    // Register the signing and encryption credentials used to protect
            //    // sensitive data like the state tokens produced by OpenIddict.
            //    options.AddDevelopmentEncryptionCertificate()
            //           .AddDevelopmentSigningCertificate();

            //    // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
            //    options.UseAspNetCore()
            //           .EnableStatusCodePagesIntegration()
            //           .EnableRedirectionEndpointPassthrough();

            //    // Register the System.Net.Http integration and use the identity of the current
            //    // assembly as a more specific user agent, which can be useful when dealing with
            //    // providers that use the user agent as a way to throttle requests (e.g Reddit).
            //    options.UseSystemNetHttp()
            //           .SetProductInformation(typeof(Program).Assembly);

            //    // Register the Web providers integrations.
            //    //
            //    // Note: to mitigate mix-up attacks, it's recommended to use a unique redirection endpoint
            //    // URI per provider, unless all the registered providers support returning a special "iss"
            //    // parameter containing their URL as part of authorization responses. For more information,
            //    // see https://datatracker.ietf.org/doc/html/draft-ietf-oauth-security-topics#section-4.4.
            //    options.UseWebProviders()
            //           .AddGitHub(options =>
            //           {
            //               options.SetClientId("c4ade52327b01ddacff3")
            //                      .SetClientSecret("da6bed851b75e317bf6b2cb67013679d9467c122")
            //                      .SetRedirectUri("callback/login/github");
            //           });
            //})

            // Register the OpenIddict server components.
            .AddServer(options =>
            {
                //options.Configure(d =>
                //{
                //    d.TokenValidationParameters.ValidIssuers = new List<string>()
                //    {
                //        "https://localhost:22235",
                //        "https://localhost:21078",
                //        "https://localhost:15117",
                //        "https://localhost:17176",
                //        "https://localhost:7283",
                //        "https://localhost:7269",
                //        "Https://localhost:7269/api",
                //        "https://localhost:7265",
                //        "https://localhost:7208",
                //        "https://localhost:7208/PushNotificationHub",
                //        "https://localhost:7108",
                //        "https://localhost:7103",
                //    };
                //});

                options.RequireProofKeyForCodeExchange();
                // Enable the authorization, logout, token and userinfo endpoints.
                options.SetAuthorizationEndpointUris("connect/authorize")
                       .SetEndSessionEndpointUris("connect/endsession")
                       .SetIntrospectionEndpointUris("connect/introspect")
                       .SetTokenEndpointUris("connect/token")
                       .SetUserInfoEndpointUris("connect/userinfo")
                       .SetEndUserVerificationEndpointUris("connect/verify");

                // Mark the "email", "profile" and "roles" scopes as supported scopes.
                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                // Note: this sample only uses the authorization code flow but you can enable
                // the other flows if you need to support implicit, password or client credentials.
                options.AllowAuthorizationCodeFlow();
                //.AllowRefreshTokenFlow(); //testing this 24/01/25

                // Register the signing and encryption credentials.
                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                options.UseAspNetCore()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableEndSessionEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserInfoEndpointPassthrough()
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

// Register the worker responsible for seeding the database.
// Note: in a real world application, this step should be part of a setup script.
builder.Services.AddHostedService<Worker>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
