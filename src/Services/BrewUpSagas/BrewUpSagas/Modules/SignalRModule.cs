using BrewUpSagas.Facade.Endpoints;

namespace BrewUpSagas.Modules;

public class SignalRModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 10;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapSignalR();
}