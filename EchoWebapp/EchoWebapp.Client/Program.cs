global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using CoreLib.WebAPI;
using EchoWebapp.Client.Provider;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices();

builder.Services.AddSingleton<AccountIdContainer>();
builder.Services.AddSingleton<EchoAPI>();
builder.Services.AddScoped<AuthenticationService>();


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
