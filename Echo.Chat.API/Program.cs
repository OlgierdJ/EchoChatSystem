using Echo.Domain.Shared.Constants;
using Echo.Chat.API.Extensions;
using Scalar.AspNetCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DomainCoreApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.AddBasicServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

//var withApiVersioning = builder.Services.AddApiVersioning();

//builder.AddDefaultOpenApi(withApiVersioning);

builder.Services.AddControllers();

var scheme = new OpenApiSecurityScheme()
{
    Type = SecuritySchemeType.Http,
    Name = JwtBearerDefaults.AuthenticationScheme,
    Scheme = JwtBearerDefaults.AuthenticationScheme,
};

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        //policy.WithOrigins("https://localhost:7269")
        policy.WithOrigins("https://localhost:7269")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

var app = builder.Build();

//app.UseCors("CorsPolicy");
app.UseCors();

//app.UseCors(EchoDomainConstants.PublicCorsPolicy);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();

//app.MapGet("/hi", () => "Hello");

//app.NewVersionedApi("Catalog")
//   .MapDomainApiV1();

//app.UseDefaultOpenApi();

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(opts =>
    {
        //opts.WithOpenApiRoutePattern("localhost:7069");
        //opts.WithProxyUrl("https://dev.echo.chat.gg");

        //var hostUrl = Environment.GetEnvironmentVariable("services__chat-api__http__0"); // <-- Now we can access the injected environment variable containing the URL of the proxy 
        //                                                                                 // http://localhost:7009 in my case

        //if (hostUrl is not null)
        //{
        //    opts.Servers = [new ScalarServer(hostUrl)];
        opts.Servers = [new ScalarServer("https://localhost:7269")];
        
        //}
        //opts.WithHttpBearerAuthentication(beareropts =>
        //{
        //});
    });
    //app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/openapi/v1.json", "Echo"));
}

app.MapHub<DomainPushNotificationHub>("DomainPushNotificationHub");

app.Run();
