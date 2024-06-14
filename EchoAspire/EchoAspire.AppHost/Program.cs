var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var echoAuthDb = builder.AddConnectionString("EchoAuthDbConnection");

var echoDb = builder.AddConnectionString("EchoDbConnection");

var echoAuthService = builder.AddProject<Projects.Echo_Auth>("echoauthservice")
    .WithExternalHttpEndpoints()
    .WithReference(echoAuthDb);

//var echoChatApiService = builder.AddProject<Projects.Echo_Chat_API>("echochatapiservice")
//    .WithReference(echoDb);

//var echoChatPushNotificationService = builder.AddProject<Projects.Echo_Chat_PushNotification>("echochatpushnotificationservice")
//    .WithReference(cache)
//    .WithReference(echoDb) //probably needs db direct access to get data without calling api.
//    .WithReference(echoChatApiService);

//var echoChatRTCService = builder.AddProject<Projects.Echo_Chat_RTC>("echochatrtcservice")
//    .WithReference(cache)
//    .WithReference(echoDb) //probably needs db direct access to get data without calling api.
//    .WithReference(echoChatApiService)
//    .WithReference(echoChatPushNotificationService);

//builder.AddProject<Projects.Echo_Chat_Web>("echochatwebservice")
//    .WithExternalHttpEndpoints()
//    .WithReference(cache)
//    .WithReference(echoChatApiService)
//    .WithReference(echoChatPushNotificationService)
//    .WithReference(echoChatRTCService);

builder.Build().Run();
