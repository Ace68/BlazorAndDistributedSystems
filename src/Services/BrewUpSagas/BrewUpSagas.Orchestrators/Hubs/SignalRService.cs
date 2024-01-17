using Microsoft.Azure.SignalR.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BrewUpSagas.Orchestrators.Hubs;

public interface IHubContextStore
{
	public ServiceHubContext BrewUpHubContext { get; }
}

public class SignalRService(IConfiguration configuration, ILoggerFactory loggerFactory) : IHostedService, IHubContextStore
{
	private const string BrewUpHub = "brewup";

	public ServiceHubContext BrewUpHubContext { get; private set; }

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		using var serviceManager = new ServiceManagerBuilder()
			.WithConfiguration(configuration)
			.WithLoggerFactory(loggerFactory)
			.BuildServiceManager();
		BrewUpHubContext = await serviceManager.CreateHubContextAsync(BrewUpHub, cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.WhenAll(Dispose(BrewUpHubContext));
	}

	protected virtual Task Dispose(IServiceHubContext hubContext)
	{
		return hubContext == null ? Task.CompletedTask : hubContext.DisposeAsync();
	}
}