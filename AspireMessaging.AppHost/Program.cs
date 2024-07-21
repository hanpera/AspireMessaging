var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var rabbitUser = builder.AddParameter("RabbitUser");
var rabbitPass = builder.AddParameter("RabbitPassword", true);
var messaging = builder.AddRabbitMQ("RabbitMQConnection", rabbitUser, rabbitPass)
    .WithManagementPlugin()
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "guest"); ;
var apiService = builder.AddProject<Projects.AspireMessaging_ApiService>("apiservice")
    .WithReference(messaging);

builder.AddProject<Projects.AspireMessaging_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(messaging);

builder.Build().Run();
