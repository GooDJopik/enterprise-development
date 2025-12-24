var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("car-rental-container")
    .AddDatabase("car-rental");

var natsLogin = builder.AddParameter("NatsLogin");
var natsPassword = builder.AddParameter("NatsPassword");

var nats = builder.AddNats("nats", port: 4222)
    .WithJetStream()
    .WithEnvironment("NATS_USER", natsLogin)
    .WithEnvironment("NATS_PASSWORD", natsPassword)
    .WithArgs("-m", "8222")
    .WithHttpEndpoint(port: 8222, targetPort: 8222);

builder.AddContainer("nats-ui", "ghcr.io/nats-nui/nui")
    .WithReference(nats)
    .WaitFor(nats)
    .WithHttpEndpoint(port: 31311, targetPort: 31311);

builder.AddProject<Projects.CarRental_Api_Host>("car-rental-api-host")
    .WithReference(db, "CarRentalDatabase")
    .WithReference(nats)
    .WaitFor(db)
    .WaitFor(nats);

builder.AddProject<Projects.CarRental_Generator_Nats_Host>("car-rental-generator")
    .WithReference(nats)
    .WaitFor(nats);

builder.Build().Run();