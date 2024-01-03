using BrewUpSales.Infrastructures;
using BrewUpSales.ReadModel.Abstracts;
using BrewUpSales.ReadModel.Entities;
using BrewUpSales.ReadModel.Queries;
using BrewUpSales.ReadModel.Services;
using BrewUpSales.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpSales.Facade;

public static class ReceiverHelper
{
    public static IServiceCollection AddReceiver(this IServiceCollection services)
    {
        services.AddScoped<IBrewOrderService, BrewOrderService>();
        services.AddScoped<IQueries<BrewOrder>, BrewOrderQueries>();
        services.AddScoped<IReceiverFacade, ReceiverFacade>();

        return services;
    }

    public static IServiceCollection AddReceiverInfrastructure(this IServiceCollection services,
        MongoDbSettings mongoDbSettings,
        RabbitMqSettings rabbitMqSettings,
        string eventStoreConnectionString)
    {
        services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreConnectionString);

        return services;
    }
}