using Microsoft.AspNetCore.OData;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.OData.ModelBuilder;
using U4PIM.InvoiceManagementAPI.Services;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Customer>("Customers");

builder.Services
    .AddSingleton((s) =>
    {
        return new CosmosClientBuilder("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==")
        .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
        .Build();
    })
    .AddSingleton<ICosmosService, CosmosService>()
    .AddControllers()
    .AddOData(options => options
        .EnableQueryFeatures()
        .AddRouteComponents(
            modelBuilder.GetEdmModel()
        )
    );


var app = builder.Build();

app
    .UseRouting()
    .UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();