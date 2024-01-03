using BrewUpLogistics.Facade;
using BrewUpLogistics.Shared.Configurations;

namespace BrewUpLogistics.Modules;

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

        builder.Services.AddLogisticsInfrastructure(mongoDbSettings, rabbitMqSettings, builder.Configuration["BrewUp:EventStore:ConnectionString"]!);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}