var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.DomainCoreApi>("coreapiservice");
var pushNotificationService = builder.AddProject<Projects.DomainPushNotificationApi>("pushnotificationservice")
    .WithReference(cache)
    .WithReference(apiService);
var rtcService = builder.AddProject<Projects.DomainRTCApi>("rtcservice")
    .WithReference(cache)
    .WithReference(pushNotificationService)
    .WithReference(apiService);


builder.AddProject<Projects.EchoWebapp>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(pushNotificationService)
    .WithReference(apiService)
    .WithReference(rtcService);

builder.Build().Run();
