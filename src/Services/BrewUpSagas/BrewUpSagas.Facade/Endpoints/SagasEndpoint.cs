using BrewUpSagas.Facade.Validators;
using BrewUpSagas.Orchestrators.Hubs;
using BrewUpSagas.Shared.BindingContracts;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUpSagas.Facade.Endpoints;

public static class SagasEndpoint
{
	public static IEndpointRouteBuilder MapSagasEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/sagas/")
			.WithTags("Sagas");

		group.MapPost("breworders", HandleSendBrewOrder)
			.WithName("SendBrewUpOrders");

		return endpoints;
	}

	public static IEndpointRouteBuilder MapSignalR(this IEndpointRouteBuilder endpoints)
	{
		endpoints.MapHub<BrewUpHub>("/brewup", options =>
		{
			options.AllowStatefulReconnects = true;
		});

		return endpoints;
	}

	public static async Task<IResult> HandleSendBrewOrder(
		ISagasFacade sagasFacade,
		IValidator<BrewOrderContract> validator,
		ValidationHandler validationHandler,
		BrewOrderContract body,
		CancellationToken cancellationToken)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		await sagasFacade.SendBrewOrderAsync(body, cancellationToken);
		return Results.Ok();
	}
}