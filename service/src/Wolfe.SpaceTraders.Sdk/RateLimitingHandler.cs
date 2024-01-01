namespace Wolfe.SpaceTraders.Sdk;

internal class RateLimitingHandler(RateLimiter limiter) : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return limiter.Limit(() => base.SendAsync(request, cancellationToken), cancellationToken);
    }
}