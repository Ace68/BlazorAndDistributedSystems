using BrewUpWarehouses.ReadModel.Abstracts;
using BrewUpWarehouses.ReadModel.Entities;
using BrewUpWarehouses.Shared.CustomTypes;
using BrewUpWarehouses.Shared.DomainIds;
using Microsoft.Extensions.Logging;

namespace BrewUpWarehouses.ReadModel.Services;

public class WarehousesService : BaseService, IWarehousesService
{
    public WarehousesService(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
    }

    public async Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName, CancellationToken cancellationToken)
    {
        var entity = Warehouses.CreateWarehouse(warehouseId, warehouseName);
        await Persister.InsertAsync(entity, cancellationToken);
    }
}