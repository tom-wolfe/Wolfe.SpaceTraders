namespace Wolfe.SpaceTraders.Sdk;

public class RateLimitingHandler(int requestCount, TimeSpan interval) : DelegatingHandler
{
    private readonly SemaphoreSlim _semaphore = new(requestCount, requestCount);

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            return await base.SendAsync(request, cancellationToken);
        }
        finally
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task
                .Delay(interval, CancellationToken.None)
                .ContinueWith(_ => _semaphore.Release(), CancellationToken.None);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}