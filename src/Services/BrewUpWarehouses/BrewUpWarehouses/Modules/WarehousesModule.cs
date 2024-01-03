using BrewUpWarehouses.Facade;
using BrewUpWarehouses.Facade.Endpoints;

namespace BrewUpWarehouses.Modules;

public class WarehousesModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder) => builder.Services.AddWarehouses();

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapWarehousesEndpoints();
}