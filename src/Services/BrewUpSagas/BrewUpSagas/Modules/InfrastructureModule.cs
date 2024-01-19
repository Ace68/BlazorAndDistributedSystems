using BrewUpSagas.Facade;
using BrewUpSagas.Shared.Configurations;

namespace BrewUpSagas.Modules;

public class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 20;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var rabbitMqSettings = builder.Configuration.GetSection("BrewUp:RabbitMqSettings")
            .Get<RabbitMqSettings>()!;
        var mongoDbSettings = builder.Configuration.GetSection("BrewUp:MongoDbSettings")
            .Get<MongoDbSettings>()!;

        builder.Services.Configure<PubSubSettings>(
            builder.Configuration.GetSection("BrewUp:PubSubSettings"));

        builder.Services.AddSagasInfrastructure(mongoDbSettings, rabbitMqSettings);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}