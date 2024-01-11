using BrewUpSagas.Messages.Commands;
using BrewUpSagas.Messages.IntegrationEvents;
using BrewUpSagas.Orchestrators.Hubs;
using BrewUpSagas.Shared.BindingContracts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace BrewUpSagas.Orchestrators.Sagas;

public class BrewOrderSaga(IServiceBus serviceBus, ISagaRepository repository, ILoggerFactory loggerFactory,
		IHubContext<BrewUpHub> hubContext)
	: Saga<BrewOrderSaga.BrewOrderSagaState>(serviceBus, repository, loggerFactory),
		ISagaStartedByAsync<StartBrewOrderSaga>,
		ISagaEventHandlerAsync<BrewOrderApproved>,
		ISagaEventHandlerAsync<BrewOrderReadyToSend>,
		ISagaEventHandlerAsync<BrewOrderProcessed>,
		ISagaEventHandlerAsync<BrewOrderSagaCompleted>
{
	public class BrewOrderSagaState
	{
		public string SagaId { get; set; } = string.Empty;
		public BrewOrderContract Body { get; set; } = new();

		public bool BrewOrderApproved { get; set; }
		public bool BrewOrderReadyToSend { get; set; }
		public bool BrewOrderProcessed { get; set; }
		public bool BrewOrderClosed { get; set; }
	}

	public async Task StartedByAsync(StartBrewOrderSaga command)
	{
		SagaState = new BrewOrderSagaState
		{
			SagaId = command.MessageId.ToString(),
			Body = new BrewOrderContract
			{
				BrewOrderNumber = command.BrewOrderNumber.Value,
				ReceivedOn = command.ReceivedOn.Value,
				BrewOrderBody = command.BrewOrderBody.Value
			}
		};
		await Repository.SaveAsync(command.MessageId, SagaState);

		var receiveBrewOrder = new ReceiveBrewOrder(command.BrewOrderId, command.MessageId, command.BrewOrderNumber,
						command.ReceivedOn, command.BrewOrderBody);
		await ServiceBus.SendAsync(receiveBrewOrder, CancellationToken.None);

		await hubContext.Clients.All.SendAsync("TellEveryoneThatBrewOrderSagaWasStarted", "Brewer", "Your BrewOrder has been Received").ConfigureAwait(false);
	}

	public async Task HandleAsync(BrewOrderApproved @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var sagaState = await Repository.GetByIdAsync<BrewOrderSagaState>(correlationId);
		sagaState.BrewOrderApproved = true;
		await Repository.SaveAsync(correlationId, sagaState);

		await hubContext.Clients.All.SendAsync("TellEveryoneThatBrewOrderWasApproved", "Brewer", "Your BrewOrder has been Approved").ConfigureAwait(false);

		await ServiceBus.SendAsync(new PrepareBrewOrder(@event.BrewOrderId, correlationId, @event.BrewOrderBody), CancellationToken.None);
	}

	public async Task HandleAsync(BrewOrderReadyToSend @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		var sagaState = await Repository.GetByIdAsync<BrewOrderSagaState>(correlationId);
		sagaState.BrewOrderReadyToSend = true;
		await Repository.SaveAsync(correlationId, sagaState);

		await ServiceBus.SendAsync(new SendBrewOrder(@event.BrewOrderId, correlationId, @event.BrewOrderBody),
			CancellationToken.None);
	}

	public async Task HandleAsync(BrewOrderProcessed @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await hubContext.Clients.All.SendAsync("TellEveryoneThatBrewOrderWasProcessed", "Brewer", "Your BrewOrder has been Processed").ConfigureAwait(false);

		var sagaState = await Repository.GetByIdAsync<BrewOrderSagaState>(correlationId);
		sagaState.BrewOrderProcessed = true;
		await Repository.SaveAsync(correlationId, sagaState);

		await ServiceBus.SendAsync(new CloseBrewOrder(@event.BrewOrderId, correlationId));
	}

	public async Task HandleAsync(BrewOrderSagaCompleted @event)
	{
		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		await hubContext.Clients.All.SendAsync("TellEveryoneThatBrewOrderSagaWasCompleted", "Brewer", "Hi! I have done my work again! See you next time.").ConfigureAwait(false);

		var sagaState = await Repository.GetByIdAsync<BrewOrderSagaState>(correlationId);
		sagaState.BrewOrderClosed = true;
		await Repository.SaveAsync(correlationId, sagaState);
	}
}