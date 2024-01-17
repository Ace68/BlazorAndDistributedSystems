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

		group.MapPost("broadcast", HandleSignalR)
			.WithName("SignalR");

		return endpoints;
	}

	public static IEndpointRouteBuilder MapSignalR(this IEndpointRouteBuilder endpoints)
	{
		endpoints.MapHub<BrewUpHub>("/brewup", options =>
		{
			options.AllowStatefulReconnects = true;
		});

		endpoints.MapPost("negotiate", HandleNegotiate);

		return endpoints;
	}

	public static async Task<IResult> HandleNegotiate(IHubContextStore contextStore)
	{
		var negotiateResponse = await contextStore.BrewUpHubContext.NegotiateAsync(new()
		{
			UserId = "brewer",
			EnableDetailedErrors = true
		});

		return Results.Ok(new Dictionary<string, string>()
		{
			{ "url", negotiateResponse.Url },
			{ "accessToken", negotiateResponse.AccessToken }
		});
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

	public static async Task<IResult> HandleSignalR(IHubService hubService)
	{
		await hubService.TellEveryoneThatBrewOrderSagaWasStarted("Brewer", "Your BrewOrder has been Received");
		await hubService.TellEveryoneThatBrewOrderWasApproved("Brewer", "Your BrewOrder has been Approved");
		await hubService.TellEveryoneThatBrewOrderWasProcessed("Brewer", "Your BrewOrder has been Processed");
		await hubService.TellEveryoneThatBrewOrderSagaWasCompleted("Brewer", "BrewOrderSaga is completed");


		return Results.NoContent();
	}
}