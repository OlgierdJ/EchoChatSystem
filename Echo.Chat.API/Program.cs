using Echo.Domain.Shared.Constants;
using Echo.Chat.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBasicServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);

var app = builder.Build();

//app.UseCors(EchoDomainConstants.PublicCorsPolicy);

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapDefaultEndpoints();

app.NewVersionedApi("Catalog")
   .MapDomainApiV1();

app.UseDefaultOpenApi();
app.Run();
