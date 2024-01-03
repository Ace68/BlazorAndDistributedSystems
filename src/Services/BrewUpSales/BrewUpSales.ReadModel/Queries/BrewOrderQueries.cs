using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.ReadModel.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace BrewUpSales.ReadModel.Queries;

public sealed class BrewOrderQueries : IQueries<BrewOrder>
{
    private IMongoDatabase _database;

    public string DatabaseName { get; private set; }

    public BrewOrderQueries(IMongoDatabase mongoDatabase)
    {
        _database = mongoDatabase;
    }

    public async Task<BrewOrder> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var collection = _database.GetCollection<BrewOrder>(nameof(BrewOrder));
        var filter = Builders<BrewOrder>.Filter.Eq("_id", id);
        return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
            ? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
            : null)!;
    }

    public async Task<PagedResult<BrewOrder>> GetByFilterAsync(Expression<Func<BrewOrder, bool>>? query, int page,
        int pageSize, CancellationToken cancellationToken)
    {
        if (--page < 0)
            page = 0;

        var collection = _database.GetCollection<BrewOrder>(nameof(BrewOrder));
        var queryable = query != null
            ? collection.AsQueryable().Where(query)
            : collection.AsQueryable();

        var count = await queryable.CountAsync(cancellationToken: cancellationToken);
        var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

        return new PagedResult<BrewOrder>(results, page, pageSize, count);
    }
}