using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone;
using Muflone.Persistence;

namespace BrewUpWarehouses.Infrastructures.MongoDb.Readmodel;

public sealed class Repository : IRepository
{
    private readonly IMongoDatabase _database;
    private readonly ILogger _logger;

    public string DatabaseName { get; private set; }

    public Repository(IMongoDatabase database, ILoggerFactory loggerFactory)
    {
        _database = database;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public void SetDatabaseName(string databaseName)
    {
        DatabaseName = databaseName;
    }

    public async Task<TAggregate> GetByIdAsync<TAggregate>(Guid id) where TAggregate : class, IAggregate
    {
        var type = typeof(TAggregate).Name;
        try
        {
            var collection = _database.GetCollection<TAggregate>(typeof(TAggregate).Name);
            var filter = Builders<TAggregate>.Filter.Eq("_id", id);
            return (await collection.CountDocumentsAsync(filter, cancellationToken: CancellationToken.None) > 0
                ? (await collection.FindAsync(filter, cancellationToken: CancellationToken.None)).First()
                : null)!;
        }
        catch (Exception e)
        {
            _logger.LogError("Insert: Error saving Aggregate: {Type}, Message: {EMessage}, StackTrace: {EStackTrace}", type,
                e.Message, e.StackTrace);
            throw;
        }
    }

    public Task<TAggregate> GetByIdAsync<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(IAggregate aggregate, Guid commitId)
    {
        throw new NotImplementedException();
    }


    public void Dispose()
    {
    }
}