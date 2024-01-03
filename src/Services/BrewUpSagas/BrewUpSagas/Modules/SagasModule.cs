using BrewUpSagas.Facade;
using BrewUpSagas.Facade.Endpoints;

namespace BrewUpSagas.Modules;

public class SagasModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder) => builder.Services.AddSagas();

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapSagasEndpoints();
}