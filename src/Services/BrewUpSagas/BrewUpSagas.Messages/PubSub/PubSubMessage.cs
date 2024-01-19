namespace BrewUpSagas.Messages.PubSub;

public record PubSubMessage(string User, string Message, string MessageType);