using BrewUpLogistics.Facade;
using BrewUpLogistics.Facade.Endpoints;

namespace BrewUpLogistics.Modules;

public class LogisticsModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder) => builder.Services.AddLogistics();

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapLogisticsEndpoints();
}