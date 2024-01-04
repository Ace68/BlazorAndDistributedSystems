using BrewUpSagas.Facade.Validators;
using BrewUpSagas.Infrastructures;
using BrewUpSagas.Messages.Commands;
using BrewUpSagas.Messages.IntegrationEvents;
using BrewUpSagas.Orchestrators.Hubs;
using BrewUpSagas.Orchestrators.Sagas;
using BrewUpSagas.Shared.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Saga;

namespace BrewUpSagas.Facade;

public static class SagasHelper
{
	public static IServiceCollection AddSagas(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<BrewOrderContractValidator>();
		services.AddSingleton<ValidationHandler>();
		services.AddScoped<ISagasFacade, SagasFacade>();

		services.AddHostedService<HubService>();

		services.AddScoped<ISagaStartedByAsync<StartBrewOrderSaga>, BrewOrderSaga>();
		services.AddScoped<ISagaEventHandlerAsync<BrewOrderApproved>, BrewOrderSaga>();
		services.AddScoped<ISagaEventHandlerAsync<BrewOrderReadyToSend>, BrewOrderSaga>();
		services.AddScoped<ISagaEventHandlerAsync<BrewOrderProcessed>, BrewOrderSaga>();
		services.AddScoped<ISagaEventHandlerAsync<BrewOrderSagaCompleted>, BrewOrderSaga>();

		return services;
	}

	public static IServiceCollection AddSagasInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings, RabbitMqSettings rabbitMqSettings)
	{
		services.AddInfrastructures(mongoDbSettings, rabbitMqSettings);

		return services;
	}
}