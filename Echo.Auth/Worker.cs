using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Echo.Auth.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace AuthTest;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        await RegisterApplicationsAsync(scope.ServiceProvider);
        await RegisterScopesAsync(scope.ServiceProvider);

        static async Task RegisterApplicationsAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

            // API
            if (await manager.FindByClientIdAsync("echo-chat-api") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "echo-chat-api",
                    ClientSecret = "C824ED28-B544-4AFC-8726-EDB54DD4264F",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor);
            }

            if (await manager.FindByClientIdAsync("echo-chat-pushnotification") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "echo-chat-pushnotification",
                    ClientSecret = "A87EDF98-0BAC-4144-A400-CE30C0A63DC0",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor);
            }

            if (await manager.FindByClientIdAsync("echo-chat-rtc") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "echo-chat-rtc",
                    ClientSecret = "B918A6B6-3C97-4CAF-BBEC-7E0E24E14B6E",
                    Permissions =
                    {
                        Permissions.Endpoints.Introspection
                    }
                };

                await manager.CreateAsync(descriptor);
            }

            // Blazor Hosted
            if (await manager.FindByClientIdAsync("echo-chat-web") is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "echo-chat-web",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Blazor code PKCE",
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:44348/callback/logout/local")
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:44348/callback/login/local")
                    },
                    ClientSecret = "FCB65446-8CDB-4ED6-9F4E-008AEA45CC68",
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "echo-chat-api",
                        Permissions.Prefixes.Scope + "echo-chat-pushnotification",
                        Permissions.Prefixes.Scope + "echo-chat-rtc",
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
            }
        }

        static async Task RegisterScopesAsync(IServiceProvider provider)
        {
            var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("echo-chat-api") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "Echo chat API access",
                    DisplayNames =
                    {
                        [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                    },
                    Name = "echo-chat-api",
                    Resources =
                    {
                        "echo-chat-api"
                    }
                });
            }

            if (await manager.FindByNameAsync("echo-chat-pushnotification") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "Echo chat Push-Notification access",
                    DisplayNames =
                    {
                        [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                    },
                    Name = "echo-chat-pushnotification",
                    Resources =
                    {
                        "echo-chat-pushnotification"
                    }
                });
            }

            if (await manager.FindByNameAsync("echo-chat-rtc") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "Echo chat RTC access",
                    DisplayNames =
                    {
                        [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                    },
                    Name = "echo-chat-rtc",
                    Resources =
                    {
                        "echo-chat-rtc"
                    }
                });
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
