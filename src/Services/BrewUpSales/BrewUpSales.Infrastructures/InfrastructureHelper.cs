using BrewUpSales.Infrastructures.MongoDb;
using BrewUpSales.Infrastructures.RabbitMq;
using BrewUpSales.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;

namespace BrewUpSales.Infrastructures;

public static class InfrastructureHelper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        MongoDbSettings mongoDbSettings,
        RabbitMqSettings rabbitMqSettings,
        string eventStoreConnectionString)
    {
        services.AddMongoDb(mongoDbSettings);
        services.AddMufloneEventStore(eventStoreConnectionString);
        services.AddRabbitMqForSagasModule(rabbitMqSettings);

        return services;
    }

}