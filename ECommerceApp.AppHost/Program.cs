var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ECommerceApp_ApiService>("apiservice");

builder.AddProject<Projects.ECommerceApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
