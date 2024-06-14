using DomainCoreApi.Handlers;
using DomainCoreApi.Hubs;
using DomainCoreApi.Swagger;
using Echo.Chat.API.Services;
using Echo.Domain.EntityFrameworkCore.DomainEvents;
using Echo.Domain.EntityFrameworkCore.EFCORE;
using Echo.Domain.EntityFrameworkCore.EFCORE.Interceptors;
using Echo.Domain.EntityFrameworkCore.Services;
using Echo.Domain.Shared.Handlers;
using Echo.Domain.Shared.Interfaces.Handlers;
using Echo.Domain.Shared.Interfaces.Repositories;
using Echo.Domain.Shared.Interfaces.Services;
using Echo.Domain.Shared.MapperProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
// Add services to the container.
builder.Services.AddProblemDetails();

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.Configure<JWTOptions>(
    builder.Configuration.GetSection(JWTOptions.Position));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("EchoDBConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<EchoDbContext>((sp, options) =>
options.UseSqlServer(connectionString).AddInterceptors(
            sp.GetRequiredService<PublishDomainEventsInterceptor>(),
            sp.GetRequiredService<PublishTransactionDomainEventsInterceptor>()
//sp.GetRequiredService<InsertOutboxMessagesInterceptor>() //dont need right now
));
// Add services to the container.
//builder.Services.AddTransient(typeof(IPushNotificationService), typeof(PushNotificationService));
builder.Services.AddAutoMapper(opts =>
{
    opts.AddProfile<EchoCoreCommonMappings>();
});

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

builder.Services.AddAuthorization();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddScoped<IPublisher, PushNotificationPublisher>();
builder.Services.AddScoped<IDomainEventService, DomainEventService>();
builder.Services.AddScoped<PublishDomainEventsInterceptor>();
builder.Services.AddScoped<PublishTransactionDomainEventsInterceptor>();
//Add Repository to the container.
//Add services to the container.
builder.Services.AddTransient<ITokenHandler, DomainCoreApi.Handlers.TokenHandler>();
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddTransient(typeof(IChatService), typeof(ChatService));
builder.Services.AddTransient(typeof(IUserGroupService), typeof(UserGroupService));

builder.Services.AddSignalR().AddJsonProtocol(opts => opts.PayloadSerializerOptions = new JsonSerializerOptions()
{
    ReferenceHandler = ReferenceHandler.Preserve,

});

builder.Services.AddTransient(typeof(IPasswordHandler), typeof(Passwordhandler));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.MapHub<DomainPushNotificationHub>("DomainPushNotificationHub");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
