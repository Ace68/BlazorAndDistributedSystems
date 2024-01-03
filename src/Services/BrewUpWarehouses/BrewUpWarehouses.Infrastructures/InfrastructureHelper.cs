using BrewUpWarehouses.Infrastructures.MongoDb;
using BrewUpWarehouses.Infrastructures.RabbitMq;
using BrewUpWarehouses.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;

namespace BrewUpWarehouses.Infrastructures;

public static class InfrastructureHelper
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        MongoDbSettings mongoDbSettings,
        RabbitMqSettings rabbitMqSettings,
        string eventStoreConnectionString)
    {
        services.AddMongoDb(mongoDbSettings);
        services.AddMufloneEventStore(eventStoreConnectionString);
        services.AddRabbitMqForWarehousesModule(rabbitMqSettings);

        return services;
    }

}