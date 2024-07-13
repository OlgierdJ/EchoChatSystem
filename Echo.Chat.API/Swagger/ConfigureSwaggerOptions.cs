using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DomainCoreApi.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "PCM", Version = "v1" });
        options.AddSecurityDefinition("Authentication", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OpenIdConnect,
            Description = "Description",
            In = ParameterLocation.Header,
            Name = HeaderNames.Authorization,
            Flows = new OpenApiOAuthFlows
            {
                ClientCredentials = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri("/connect/token", UriKind.Relative),
                    TokenUrl = new Uri("/connect/token", UriKind.Relative)
                }
            },
            OpenIdConnectUrl = new Uri("/.well-known/openid-configuration", UriKind.Relative)
        });

        options.AddSecurityRequirement(
                            new OpenApiSecurityRequirement
                            {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                        { Type = ReferenceType.SecurityScheme, Id = "oauth" },
                                },
                                Array.Empty<string>()
                            }
                            }
                        );


        //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //{
        //    In = ParameterLocation.Header,
        //    Description = "Please provide a valid token",
        //    Name = "Authorization",
        //    Type = SecuritySchemeType.Http,
        //    BearerFormat = "JWT",
        //    Scheme = "Bearer"
        //});

        //options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type = ReferenceType.SecurityScheme,
        //                Id = "Bearer"
        //            }
        //        },
        //        Array.Empty<string>()
        //    }
        //});
    }
}
