using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;
using OpenIddict.Validation.AspNetCore;

namespace Echo.SystemOrchestrator.ServiceDefaults;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddOIDCAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

    //    var connectionString = builder.Configuration.GetConnectionString("DbConnection")
    //?? throw new InvalidOperationException($"Connection string '{"DbConnection"}' not found.");


        var serverInstanceName = builder.Configuration.GetValue<string>("ServerInstanceName")
            ?? throw new InvalidOperationException($"section '{"ServerInstanceName"}' not found.");

        var clientSecret = builder.Configuration.GetValue<string>("ClientSecret")
            ?? throw new InvalidOperationException($"section '{"ClientSecret"}' not found.");

        //var authService = builder.Configuration.GetValue<string>("AuthService")
        //    ?? throw new InvalidOperationException($"section '{"AuthService"}' not found.");

        var identitySection = configuration.GetSection("Identity");
        //var audience = identitySection.GetRequiredValue("Audience");
        if (!identitySection.Exists())
        {
            // No identity section, so no authentication
            return services;
        }

        var identityUrl = identitySection.GetRequiredValue("Url");

        services.AddOpenIddict()
    .AddValidation(options =>
    {
        // Note: the validation handler uses OpenID Connect discovery
        // to retrieve the address of the introspection endpoint.
        //options.SetIssuer("https://echoauthservice");
        //options.SetIssuer("https://localhost:7103");
        options.SetIssuer(identityUrl);
        //options.AddAudiences("resource_server_1");
        options.AddAudiences(serverInstanceName);

        // Configure the validation handler to use introspection and register the client
        // credentials used when communicating with the remote introspection endpoint.
        options.UseIntrospection()
               .SetClientId(serverInstanceName)
               //.SetClientSecret("846B62D0-DEF9-4215-A99D-86E6B8DAB342");
               .SetClientSecret(clientSecret);

        // Register the System.Net.Http integration.
        options.UseSystemNetHttp();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

        services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);


        services.AddAuthorization();

        return services;
    }
    public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        // {
        //   "Identity": {
        //     "Url": "http://identity",
        //     "Audience": "basket"
        //    }
        // }

        var identitySection = configuration.GetSection("Identity");

        if (!identitySection.Exists())
        {
            // No identity section, so no authentication
            return services;
        }

        // prevent from mapping "sub" claim to nameidentifier.
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        services.AddAuthentication().AddJwtBearer(options =>
        {
            var identityUrl = identitySection.GetRequiredValue("Url");
            var audience = identitySection.GetRequiredValue("Audience");

            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = audience;
            
#if DEBUG
            //Needed if using Android Emulator Locally. See https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/local-web-services?view=net-maui-8.0#android
            options.TokenValidationParameters.ValidIssuers = [identityUrl, "https://10.0.2.2:5243"];
#else
            options.TokenValidationParameters.ValidIssuers = [identityUrl];
#endif
            
            options.TokenValidationParameters.ValidateAudience = false;
        });

        services.AddAuthorization();

        return services;
    }
}
