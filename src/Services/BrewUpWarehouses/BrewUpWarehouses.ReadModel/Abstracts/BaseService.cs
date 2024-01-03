using Microsoft.Extensions.Logging;

namespace BrewUpWarehouses.ReadModel.Abstracts;

public abstract class BaseService
{
    protected readonly IPersister Persister;
    protected readonly ILogger Logger;

    protected BaseService(IPersister persister, ILoggerFactory loggerFactory)
    {
        Persister = persister ?? throw new ArgumentNullException(nameof(persister));
        Logger = loggerFactory.CreateLogger(GetType()) ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
}