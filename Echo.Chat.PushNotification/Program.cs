using DomainPushNotificationApi.Hubs;
using DomainPushNotificationApi.Services;
using Echo.Application.Contracts.Interfaces.Providers;
using Echo.Domain.Shared.EchoChatApiServiceServerClients;
using Echo.Domain.Shared.MapperProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.Services.AddHttpClient<EchoChatApiServiceServerClient>(client =>
{
    // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
    // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
    client.BaseAddress = new("https+http://coreapiservice");
});
// Add services to the container.
var config = builder.Configuration;
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(opts =>
{
    opts.AddProfile<EchoCoreCommonMappings>();
});
builder.Services.AddSingleton<ITokenProvider, TokenStore>();
builder.Services.AddSingleton<PushNotificationClientConnectionStore>();
builder.Services.AddSingleton<PushNotificationService>();
builder.Services.AddSingleton<DomainNotificationClientService>();
//builder.Services.AddHttpClient("DomainClient", e => 
//{
//    e.BaseAddress = new Uri("https://localhost:7269/api");
//    e.DefaultRequestHeaders.Accept.Clear();
//    e.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//});
builder.Services.AddHostedService<StartupBackgroundService>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new()
    {
        NameClaimType = ClaimTypes.NameIdentifier,
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudiences = config.GetSection("JwtSettings:Audiences").Get<List<string>>(),
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
        //.SetIsOriginAllowed((host) => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseOutputCache();

app.MapHub<PushNotificationHub>("PushNotificationHub");

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();

app.Run();