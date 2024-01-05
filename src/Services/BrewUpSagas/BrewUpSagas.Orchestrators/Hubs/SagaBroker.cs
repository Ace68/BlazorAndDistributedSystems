using System.Collections.ObjectModel;

namespace BrewUpSagas.Orchestrators.Hubs;

public static class SagaBroker
{
	public static ObservableCollection<OutMessage> MessagesOutbox { get; set; } = new();

	public static void Publish(string user, string message, string method)
	{
		MessagesOutbox.Add(new OutMessage(user, message, method));
	}
}