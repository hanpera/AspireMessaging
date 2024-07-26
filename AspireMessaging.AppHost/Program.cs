var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var rabbitUser = builder.AddParameter("RabbitUser");
var rabbitPass = builder.AddParameter("RabbitPassword", true);
var messaging = builder.AddRabbitMQ("RabbitMQConnection", rabbitUser, rabbitPass)
    .WithManagementPlugin()
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest");

var azureBus = builder.ExecutionContext.IsPublishMode
    ? builder.AddAzureServiceBus("messaging")
    : builder.AddConnectionString("messaging");

//var azureBus = builder.AddAzureServiceBus("messaging");
   
var apiService = builder.AddProject<Projects.AspireMessaging_ApiService>("apiservice")
    //.WithReference(messaging)
    .WithReference(azureBus);

builder.AddProject<Projects.AspireMessaging_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    //.WithReference(messaging)
    .WithReference(azureBus);

builder.Build().Run();
