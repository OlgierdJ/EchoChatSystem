using Echo.SystemOrchestrator.AppHost;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");
builder.AddForwardedHeaders();

var redis = builder.AddRedis("cache")
    .WithLifetime(ContainerLifetime.Persistent); ;//.WithDataVolume();

var sqlPassword = builder.AddParameter("sql-password", secret: true);
var sqlServer = builder.AddSqlServer("sql", password: sqlPassword, 1433)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var domainDb = sqlServer.AddDatabase("domaindb");
var identityDb = sqlServer.AddDatabase("identitydb");

//var orleans = builder.AddOrleans("default")
//    .WithClustering(redis)
//    .WithGrainStorage(redis);

var rabbitMqUsername = builder.AddParameter("rabbitmq-username", secret: true);
var rabbitMqPassword = builder.AddParameter("rabbitmq-password", secret: true);
var rabbitMq = builder.AddRabbitMQ("eventbus", rabbitMqUsername, rabbitMqPassword)
    .WithLifetime(ContainerLifetime.Persistent); ;

var serverInstanceParamName = "ServerInstanceName";
var clientSecretParamName = "ClientSecret";


//Services
var identityApi = builder.AddProject<Projects.Echo_Auth>("identity-api"/*, "echoauthserver1"*/)
    .WithReference(identityDb)
    .WithReference(rabbitMq)
    .WithExternalHttpEndpoints()
    .WithEnvironment(clientSecretParamName, "846B62D0-DEF9-4215-A99D-86E6B8DAB342")
    .WaitFor(identityDb)
    .WaitFor(rabbitMq);
var identityEndpoint = identityApi.GetEndpoint("https");
var identityEnvVarName = "Identity__Url";

builder.AddProject<Projects.Echo_Domain_MigrationService>("echo-domain-migrationservice")
    .WithReference(domainDb)
    .WaitFor(domainDb);

//var echoChatEventNotificationService = builder.AddProject<Projects.Echo_Orleans_Server>("chat-eventnotification")
//    .WithReference(domainDb)
//    //.WithReference(orleans)
//    .WithEnvironment(identityEnvVarName, identityEndpoint)
//    .WithEnvironment(serverInstanceParamName, "chat-eventnotification")
//    .WithEnvironment(clientSecretParamName, "A87EDB98-0BAC-4144-A400-CE30C0A63DC0");

//chat system
var echoChatApiService = builder.AddProject<Projects.Echo_Chat_API>("chat-api")
    //.WithReference(echoChatEventNotificationService)
    .WithReference(domainDb)
    .WithReference(rabbitMq)
    .WithEnvironment(identityEnvVarName, identityEndpoint)
    .WithEnvironment(serverInstanceParamName, "chat-api")
    .WithEnvironment(clientSecretParamName, "C824ED28-B544-4AFC-8726-EDB54DD4264F")
    .WaitFor(domainDb)
    .WaitFor(rabbitMq);

//var echoChatPushNotificationService = builder.AddProject<Projects.Echo_Chat_PushNotification>("chat-pushnotification")
//    .WithReference(redis)
//    //.WithReference(echoChatEventNotificationService)
//    .WithReference(rabbitMq)
//    .WithReference(domainDb)
//    .WithReference(echoChatApiService)
//    .WithEnvironment(identityEnvVarName, identityEndpoint)
//    .WithEnvironment(serverInstanceParamName, "chat-pushnotification")
//    .WithEnvironment(clientSecretParamName, "A87EDF98-0BAC-4144-A400-CE30C0A63DC0")
//    .WaitFor(redis)
//    .WaitFor(rabbitMq)
//    .WaitFor(domainDb);

////var echoChatRTCService = builder.AddProject<Projects.Echo_Chat_RTC>("echo-chat-rtc")
////    .WithReference(redis)
////    .WithReference(rabbitMq)
////    .WithReference(domainDb)
////    .WithEnvironment(identityEnvVarName, identityEndpoint)
////    //.WithReference(echoChatApiService)
////    .WithReference(echoChatPushNotificationService)
////    .WithEnvironment(serverInstanceParamName, "echo-chat-rtc")
////    .WithEnvironment(clientSecretParamName, "B918A6B6-3C97-4CAF-BBEC-7E0E24E14B6E");

////var echoChatWebService = builder.AddProject<Projects.Echo_Chat_Web>(chatWebService)
////    .WithExternalHttpEndpoints()
////    //.WithReference(cache)
////    .WithReference(echoDb) //probably needs db direct access to get data without calling api.
////    .WithReference(echoAuthService)
////    .WithReference(echoChatApiService)
////    .WithReference(echoChatPushNotificationService)
////    .WithReference(echoChatRTCService)
////    .WithEnvironment(InstanceScopesParamName, "echo_chat_api, echo_chat_pushnotification, echo_chat_rtc")
////    .WithEnvironment(authServerParamName, $"https://{authServer}")
////    .WithEnvironment(serverInstanceParamName, "echo_chat_web")
////    .WithEnvironment(clientSecretParamName, "FCB65446-8CDB-4ED6-9F4E-008AEA45CC68");


////var echoChatRTCService = builder.AddProject<Projects.Echo_Chat_RTC>("echo-chat-rtc")
////    .WithReference(redis)
////    .WithReference(rabbitMq)
////    .WithReference(domainDb)
////    .WithEnvironment(identityEnvVarName, identityEndpoint)
////    //.WithReference(echoChatApiService)
////    .WithReference(echoChatPushNotificationService)
////    .WithEnvironment(serverInstanceParamName, "echo-chat-rtc")
////    .WithEnvironment(clientSecretParamName, "B918A6B6-3C97-4CAF-BBEC-7E0E24E14B6E");

////var echoChatWebService = builder.AddProject<Projects.Echo_Chat_Web>(chatWebService)
////    .WithExternalHttpEndpoints()
////    //.WithReference(cache)
////    .WithReference(echoDb) //probably needs db direct access to get data without calling api.
////    .WithReference(echoAuthService)
////    .WithReference(echoChatApiService)
////    .WithReference(echoChatPushNotificationService)
////    .WithReference(echoChatRTCService)
////    .WithEnvironment(InstanceScopesParamName, "echo_chat_api, echo_chat_pushnotification, echo_chat_rtc")
////    .WithEnvironment(authServerParamName, $"https://{authServer}")
////    .WithEnvironment(serverInstanceParamName, "echo_chat_web")
////    .WithEnvironment(clientSecretParamName, "FCB65446-8CDB-4ED6-9F4E-008AEA45CC68");

builder.Build().Run();
