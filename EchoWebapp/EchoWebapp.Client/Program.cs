global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using CoreLib.WebAPI;
using EchoWebapp.Client.Provider;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using CoreLib.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddSingleton<AccountIdContainer>();
builder.Services.AddSingleton<IUserContainer, UserContainer>();
builder.Services.AddSingleton<EchoAPI>();
builder.Services.AddSingleton<SignalRClientService>();
//builder.Services.AddScoped<AuthenticationService>();


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


var host = builder.Build();
var connSingleton = host.Services.GetRequiredService<IUserContainer>();
await connSingleton.InitializeAsync();

// Now we can call RunAsync to render the first page component
await host.RunAsync();