using BrewApp.Modules.Orders.Extensions.Contracts;

namespace BrewApp.Modules.Orders.Extensions.Services;

public interface IBrewOrderService
{
	Task SendBrewOrderAsync(BrewOrderJson brewOrder);
}