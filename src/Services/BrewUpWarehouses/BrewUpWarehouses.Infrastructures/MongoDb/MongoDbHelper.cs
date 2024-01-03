using BrewUpWarehouses.Infrastructures.MongoDb.Readmodel;
using BrewUpWarehouses.ReadModel.Abstracts;
using BrewUpWarehouses.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUpWarehouses.Infrastructures.MongoDb;

public static class MongoDbHelper
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services,
        MongoDbSettings mongoDbSettings)
    {
        services.AddSingleton<IMongoDatabase>(x =>
        {
            var client = new MongoClient(mongoDbSettings.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.DatabaseName);
            return database;
        });

        services.AddScoped<IPersister, Persister>();

        services.AddSingleton<IEventStorePositionRepository>(x =>
            new EventStorePositionRepository(x.GetRequiredService<ILogger<EventStorePositionRepository>>(), mongoDbSettings));

        return services;
    }
}