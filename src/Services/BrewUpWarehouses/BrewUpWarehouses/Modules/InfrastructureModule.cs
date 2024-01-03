using BrewUpWarehouses.Facade;
using BrewUpWarehouses.Shared.Configurations;

namespace BrewUpWarehouses.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 10;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var rabbitMqSettings = builder.Configuration.GetSection("BrewUp:RabbitMqSettings")
            .Get<RabbitMqSettings>()!;
        var mongoDbSettings = builder.Configuration.GetSection("BrewUp:MongoDbSettings")
            .Get<MongoDbSettings>()!;

        builder.Services.AddWarehousesInfrastructure(mongoDbSettings, rabbitMqSettings, builder.Configuration["BrewUp:EventStore:ConnectionString"]!);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}