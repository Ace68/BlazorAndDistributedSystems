using BrewApp.Shared.Abstracts;
using BrewApp.Shared.Concretes;
using BrewApp.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace BrewApp.Shared.Helpers;

public static class ApplicationHelper
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpService, HttpService>()
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy())
            .SetHandlerLifetime(TimeSpan.FromMinutes(2));
        services.AddScoped<ILocalStorageService, LocalStorageService>();

        services.AddScoped<ToastService>();

        services.AddSingleton<AppState>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(3, TimeSpan.FromMinutes(1));
    }
}
