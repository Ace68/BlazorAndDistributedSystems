using BrewApp.Shared.Configuration;
using Microsoft.Extensions.Logging;

namespace BrewApp.Shared.Concretes;

public abstract class BaseService
{
    protected ILogger Logger;
    protected AppConfiguration AppConfiguration;

    protected BaseService(ILoggerFactory loggerFactory,
        AppConfiguration appConfiguration)
    {
        AppConfiguration = appConfiguration;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}