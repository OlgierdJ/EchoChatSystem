global using Microsoft.AspNetCore.Components.Authorization;
using CoreLib.WebAPI;
using EchoWebapp.Client.Provider;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
//builder.Services.AddSingleton<AccountIdContainer>();
//builder.Services.AddSingleton<EchoAPI>();
//builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthStuteProvider>();
//builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
