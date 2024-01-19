namespace BrewApp.Shared.Messages;

public record PubSubMessage(string User, string Message, string MessageType);