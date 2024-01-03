using BrewUpSagas.Orchestrators.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BrewUpSagas.Middleware;

/// <summary>
/// 
/// </summary>
public class ResolveHubContextMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public ResolveHubContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="hubService"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context,
        IHubService hubService)
    {
        var hubContext = context.RequestServices.GetService<IHubContext<BrewUpHub, IHubsHelper>>();
        hubService.RegisterHubContext(hubContext!);

        await _next(context);
    }
}
