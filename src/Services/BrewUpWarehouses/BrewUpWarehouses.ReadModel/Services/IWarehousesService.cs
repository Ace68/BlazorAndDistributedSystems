using BrewUpWarehouses.Shared.CustomTypes;
using BrewUpWarehouses.Shared.DomainIds;

namespace BrewUpWarehouses.ReadModel.Services;

public interface IWarehousesService
{
    Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName, CancellationToken cancellationToken);
}