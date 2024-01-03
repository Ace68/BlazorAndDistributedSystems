using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.ReadModel.Entities;
using BrewUpSales.Shared.BindingContracts;
using BrewUpSales.Shared.CustomTypes;
using BrewUpSales.Shared.DomainIds;
using BrewUpSales.Shared.Enums;
using Microsoft.Extensions.Logging;

namespace BrewUpSales.ReadModel.Services;

public sealed class BrewOrderService(IPersister persister, ILoggerFactory loggerFactory, IQueries<BrewOrder> queries)
    : BaseService(persister, loggerFactory), IBrewOrderService
{
    public async Task ReceiveBrewOrderAsync(BrewOrderId aggregateId, BrewOrderNumber brewOrderNumber, ReceivedOn receivedOn,
        BrewOrderBody brewOrderBody, BrewOrderStatus brewOrderStatus,
        CancellationToken cancellationToken = default)
    {
        var entity = await Persister.GetByIdAsync<BrewOrder>(aggregateId.Value.ToString(), cancellationToken);
        if (entity != null && !string.IsNullOrWhiteSpace(entity.BrewOrderNumber))
            return;

        entity = BrewOrder.CreateBrewOrder(aggregateId, brewOrderNumber, receivedOn, brewOrderBody, brewOrderStatus);
        await Persister.InsertAsync(entity, cancellationToken);
    }

    public async Task<PagedResult<BrewOrderContract>> GetBrewOrdersAsync(CancellationToken cancellationToken)
    {
        var results = await queries.GetByFilterAsync(null, 0, 200, cancellationToken);

        return new PagedResult<BrewOrderContract>(results.Results.Select(r => r.ToJson()), results.Page,
            results.PageSize, results.TotalRecords);
    }

    public async Task CloseBrewOrderAsync(BrewOrderId brewOrderId, CancellationToken cancellationToken)
    {
        var entity = await Persister.GetByIdAsync<BrewOrder>(brewOrderId.Value.ToString(), cancellationToken);
        entity.CloseBrewOrder();
        await Persister.UpdateAsync(entity, cancellationToken);
    }
}