using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUpLogistics.Facade.Endpoints;

public static class LogisticsEndpoints
{
	public static IEndpointRouteBuilder MapLogisticsEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/logistics/")
			.WithTags("Logistics");

		group.MapGet("", () => Results.Ok());

		return endpoints;
	}
}