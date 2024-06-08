using CoreLib.Handlers;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using CoreLib.MapperProfiles;
using DomainCoreApi.EFCORE;
using DomainCoreApi.EFCORE.Interceptors;
using DomainCoreApi.Handlers;
using DomainCoreApi.Hubs;
using DomainCoreApi.Repositories;
using DomainCoreApi.Services;
using DomainCoreApi.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

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
        //ValidAudience = config["JwtSettings:Audience"],
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
builder.Services.AddScoped<DomainEventService>();
builder.Services.AddScoped<PublishDomainEventsInterceptor>();
builder.Services.AddScoped<PublishTransactionDomainEventsInterceptor>();
//Add Repository to the container.
builder.Services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddTransient(typeof(IAccountRepository), typeof(AccountRepository));
builder.Services.AddTransient(typeof(ISecurityCredentialsRepository), typeof(SecurityCredentialsRepository));
builder.Services.AddTransient(typeof(ILanguageRepository), typeof(LanguageRepository));
//Add services to the container.
builder.Services.AddTransient<DomainCoreApi.Handlers.TokenHandler>();
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddTransient(typeof(IChatService), typeof(ChatService));

builder.Services.AddSignalR();

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
