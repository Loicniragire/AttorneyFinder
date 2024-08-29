using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
namespace Attorneys.Middlewares;

public class CachingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
	private readonly CacheSettings _cacheSettings;

	public CachingMiddleware(RequestDelegate next, IOptions<CacheSettings> cacheSettings)
	{
		_next = next;
		_cacheSettings = cacheSettings.Value;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		var cacheKey = context.Request.Path.ToString();
		if (_cache.TryGetValue(cacheKey, out var cachedResponse))
		{
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsync(cachedResponse.ToString());
			return;
		}

		// Capture the response body
		var originalBodyStream = context.Response.Body;
		using var newBodyStream = new MemoryStream();
		context.Response.Body = newBodyStream;

		await _next(context);

		if (context.Response.StatusCode == 200)
		{
			newBodyStream.Seek(0, SeekOrigin.Begin);
			var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
			_cache.Set(cacheKey, responseBody, TimeSpan.FromMinutes(_cacheSettings.TTLMinutes));
			newBodyStream.Seek(0, SeekOrigin.Begin);
			await newBodyStream.CopyToAsync(originalBodyStream);
		}
	}

}

