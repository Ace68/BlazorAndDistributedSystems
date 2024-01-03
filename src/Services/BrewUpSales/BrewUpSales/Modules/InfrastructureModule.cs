using BrewUpSales.Facade;
using BrewUpSales.Shared.Configurations;

namespace BrewUpSales.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 10;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var mongoDbSettings = builder.Configuration.GetSection("BrewUp:MongoDbSettings")
            .Get<MongoDbSettings>()!;
        var rabbitMqSettings = builder.Configuration.GetSection("BrewUp:RabbitMqSettings")
            .Get<RabbitMqSettings>()!;

        builder.Services.AddReceiverInfrastructure(mongoDbSettings, rabbitMqSettings, builder.Configuration["BrewUp:EventStore:ConnectionString"]!);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}