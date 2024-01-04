namespace BrewUpSagas.Orchestrators.Hubs;

public record OutMessage(string User, string Message, string Method);