using BrewUpSales.Infrastructures.MongoDb.Readmodel;
using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUpSales.Infrastructures.MongoDb;

public static class MongoDbHelper
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services,
        MongoDbSettings mongoDbSettings)
    {
        services.AddSingleton<IMongoDatabase>(x =>
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

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