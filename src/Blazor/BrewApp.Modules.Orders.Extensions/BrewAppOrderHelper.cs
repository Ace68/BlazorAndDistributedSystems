using BrewApp.Modules.Orders.Extensions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BrewApp.Modules.Orders.Extensions;

public static class BrewAppOrderHelper
{
	public static IServiceCollection AddBrewAppOrders(this IServiceCollection services)
	{
		services.AddScoped<IBrewOrderService, BrewOrderService>();

		return services;
	}
}