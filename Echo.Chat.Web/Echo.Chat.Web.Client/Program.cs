global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
using Echo.Application.Clients.EchoChatAPIServiceClients;
using Echo.Application.Clients.EchoChatPushNotificationServiceClients;
using Echo.Chat.Web.Client.Provider;
using EchoWebapp.Client.Provider;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddHttpClient<EchoChatApiServiceClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://coreapiservice");
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddSingleton<AccountIdContainer>();
builder.Services.AddSingleton<IUserContainer, UserContainer>();
//builder.Services.AddSingleton<EchoAPI>();
builder.Services.AddSingleton<EchoChatPushNotificationServiceClient>();
//builder.Services.AddScoped<AuthenticationService>();


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


var host = builder.Build();
var connSingleton = host.Services.GetRequiredService<IUserContainer>();
await connSingleton.InitializeAsync();

// Now we can call RunAsync to render the first page component
await host.RunAsync();