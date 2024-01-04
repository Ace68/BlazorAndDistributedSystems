using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.ReadModel.Services;
using BrewUpSales.Shared.BindingContracts;

namespace BrewUpSales.Facade;

public sealed class ReceiverFacade(IBrewOrderService brewOrderService) : IReceiverFacade
{
	public async Task<PagedResult<BrewOrderContract>> GetBrewOrdersAsync(CancellationToken cancellationToken)
	{
		return await brewOrderService.GetBrewOrdersAsync(cancellationToken);
	}
}