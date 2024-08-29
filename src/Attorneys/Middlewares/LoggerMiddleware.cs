using System.Diagnostics;
namespace Attorneys;
public class LoggerMiddleware
{
    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var stopwatch = Stopwatch.StartNew();

        // Log request details
        Console.WriteLine($"Request: {request.Method} {request.Path}");

        await _next(context);

        stopwatch.Stop();
        // Log response details
        Console.WriteLine($"Response: {context.Response.StatusCode} took {stopwatch.ElapsedMilliseconds}ms");
    }
}

