using BrewUpWarehouses.Infrastructures;
using BrewUpWarehouses.ReadModel.Services;
using BrewUpWarehouses.Shared.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUpWarehouses.Facade;

public static class WarehousesHelper
{
    public static IServiceCollection AddWarehouses(this IServiceCollection services)
    {
        services.AddScoped<IWarehousesService, WarehousesService>();
        services.AddScoped<IWarehousesFacade, WarehousesFacade>();

        return services;
    }

    public static IServiceCollection AddWarehousesInfrastructure(this IServiceCollection services,
        MongoDbSettings mongoDbSettings,
        RabbitMqSettings rabbitMqSettings,
        string eventStoreConnectionString)
    {
        services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreConnectionString);
        return services;
    }
}