using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUpSales.Facade.Endpoints;

public static class ReceiverEndpoints
{
	public static IEndpointRouteBuilder MapReceiverEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/receivers/")
			.WithTags("Receivers");

		group.MapGet("breworders", HandleGetBrewOrders)
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status500InternalServerError)
			.WithName("GetBrewOrders");

		return endpoints;
	}

	public static async Task<IResult> HandleGetBrewOrders(IReceiverFacade receiverFacade,
		CancellationToken cancellationToken)
	{
		var brewOrdersResult = await receiverFacade.GetBrewOrdersAsync(cancellationToken);

		return Results.Ok(brewOrdersResult.Results);
	}
}