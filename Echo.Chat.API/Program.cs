using Echo.Domain.Shared.Constants;
using Echo.Chat.API.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddBasicServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

//var withApiVersioning = builder.Services.AddApiVersioning();

//builder.AddDefaultOpenApi(withApiVersioning);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

//app.UseCors(EchoDomainConstants.PublicCorsPolicy);

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapControllers();

app.MapGet("/hi", () => "Hello");

//app.NewVersionedApi("Catalog")
//   .MapDomainApiV1();

//app.UseDefaultOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opts =>
    {
        opts.Servers = [];
    });
}

app.Run();
