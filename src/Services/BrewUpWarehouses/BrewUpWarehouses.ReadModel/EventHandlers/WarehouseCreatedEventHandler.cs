using BrewUpWarehouses.Messages.Events;
using BrewUpWarehouses.ReadModel.Services;
using BrewUpWarehouses.Shared.CustomTypes;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUpWarehouses.ReadModel.EventHandlers;

public sealed class WarehouseCreatedEventHandler(ILoggerFactory loggerFactory, IWarehousesService warehousesService)
    : DomainEventHandlerAsync<WarehouseCreated>(loggerFactory)
{
    public override async Task HandleAsync(WarehouseCreated @event, CancellationToken cancellationToken = new())
    {
        await warehousesService.CreateWarehouseAsync(@event.WarehouseId, new WarehouseName("New Warehouse"), cancellationToken);
    }
}