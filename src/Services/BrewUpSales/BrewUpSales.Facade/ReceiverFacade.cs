using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.ReadModel.Services;
using BrewUpSales.Shared.BindingContracts;

namespace BrewUpSales.Facade;

public sealed class ReceiverFacade(IBrewOrderService xmasLetterService) : IReceiverFacade
{
    public async Task<PagedResult<BrewOrderContract>> GetBrewOrdersAsync(CancellationToken cancellationToken)
    {
        return await xmasLetterService.GetBrewOrdersAsync(cancellationToken);
    }
}