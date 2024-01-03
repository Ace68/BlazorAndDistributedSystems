using BrewUpLogistics.Infrastructures.MongoDb;
using BrewUpLogistics.Infrastructures.RabbitMq;
using BrewUpLogistics.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;

namespace BrewUpLogistics.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		string eventStoreConnectionString)
	{
		services.AddMongoDb(mongoDbSettings);
		services.AddMufloneEventStore(eventStoreConnectionString);
		services.AddRabbitMqForLogisticsModule(rabbitMqSettings);

		return services;
	}

}