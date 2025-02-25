using System.Globalization;

namespace DDDProject.API.Middlewares;

public class CultureMiddleware
{
private readonly RequestDelegate _next;

public CultureMiddleware(RequestDelegate next)
{
    _next = next;
}

public async Task InvokeAsync(HttpContext context)
{
    // Check for culture in headers, cookies, or query string
    var cultureName = context.Request.Headers["Accept-Language"].FirstOrDefault() ??
                      context.Request.Cookies["culture"] ??
                      context.Request.Query["culture"].FirstOrDefault() ??
                      "fr"; // Default to French

    // Set culture
    var culture = new CultureInfo(cultureName);
    CultureInfo.CurrentCulture = culture;
    CultureInfo.CurrentUICulture = culture;

    // Call the next delegate/middleware in the pipeline
    await _next(context);
}
}

// Extension method to add the middleware to the pipeline
public static class CultureMiddlewareExtensions
{
    public static IApplicationBuilder UseCultureMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CultureMiddleware>();
    }
}
