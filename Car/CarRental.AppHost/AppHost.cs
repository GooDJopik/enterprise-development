var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("car-rental-container")
    .AddDatabase("car-rental");

builder.AddProject<Projects.CarRental_Api_Host>("car-rental-api-host")
    .WithReference(db, "CarRentalDatabase")
    .WaitFor(db);

builder.Build().Run();