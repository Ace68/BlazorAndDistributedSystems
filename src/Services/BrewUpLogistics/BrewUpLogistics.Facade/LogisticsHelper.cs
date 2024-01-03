using BrewUpLogistics.Infrastructures;
using BrewUpLogistics.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpLogistics.Facade;

public static class LogisticsHelper
{
	public static IServiceCollection AddLogistics(this IServiceCollection services)
	{
		services.AddScoped<ILogisticsFacade, LogisticsFacade>();

		return services;
	}

	public static IServiceCollection AddLogisticsInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreConnectionString);
		return services;
	}
}