using BrewUpSagas.Facade.Endpoints;
using BrewUpSagas.Orchestrators.Hubs;

namespace BrewUpSagas.Modules;

public class SignalRModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddSignalR(options =>
			{
				options.EnableDetailedErrors = true;
				options.KeepAliveInterval = TimeSpan.FromSeconds(30);
				options.HandshakeTimeout = TimeSpan.FromSeconds(30);
			})
			.AddAzureSignalR(builder.Configuration["Azure:SignalR:ConnectionString"]);

		builder.Services.AddSingleton<SignalRService>()
			.AddHostedService(s => s.GetService<SignalRService>())
			.AddSingleton<IHubContextStore>(sp => sp.GetService<SignalRService>());

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints.MapSignalR();
}