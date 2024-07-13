

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
//builder.AddBasicServiceDefaults();
builder.AddApplicationServices();
builder.Services.AddProblemDetails();
//builder.AddRedisOutputCache("cache");

var withApiVersioning = builder.Services.AddApiVersioning();

builder.AddDefaultOpenApi(withApiVersioning);


var app = builder.Build();

//app.UseCors(EchoDomainConstants.PublicCorsPolicy);

//app.UseHttpsRedirection();
//app.UseOutputCache();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapHub<PushNotificationHub>("PushNotificationHub");

app.MapDefaultEndpoints();

app.NewVersionedApi("PushNotificationApi")
   .MapPushNotificationApiV1();

app.UseDefaultOpenApi();

app.Run();
