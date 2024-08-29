namespace Attorneys;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly Dictionary<string, int> _requests = new Dictionary<string, int>();
    private static readonly TimeSpan _resetInterval = TimeSpan.FromMinutes(1);
    private static readonly int _requestLimit = 100;

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress.ToString();

        if (!_requests.ContainsKey(clientIp))
        {
            _requests[clientIp] = 0;
            Task.Delay(_resetInterval).ContinueWith(_ => _requests.Remove(clientIp));
        }

        _requests[clientIp]++;

        if (_requests[clientIp] > _requestLimit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
            return;
        }

        await _next(context);
    }
}


