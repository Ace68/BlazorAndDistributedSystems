using BrewUpSagas.Infrastructures.MongoDb;
using BrewUpSagas.Infrastructures.RabbitMq;
using BrewUpSagas.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpSagas.Infrastructures;

public static class InfrastructureHelper
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services,
        MongoDbSettings mongoDbSettings, RabbitMqSettings rabbitMqSettings)
    {
        services.AddMongoDb(mongoDbSettings);
        services.AddRabbitMqForSagasModule(rabbitMqSettings);

        return services;
    }
}