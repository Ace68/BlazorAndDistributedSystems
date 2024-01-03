using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.Shared.BindingContracts;

namespace BrewUpSales.Facade;

public interface IReceiverFacade
{
    Task<PagedResult<BrewOrderContract>> GetBrewOrdersAsync(CancellationToken cancellationToken);
}