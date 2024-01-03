namespace BrewUpSagas.Middleware;

/// <summary>
/// 
/// </summary>
public static class ResolveHubContextMiddlewareExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseResolveHubContext(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ResolveHubContextMiddleware>();
    }
}