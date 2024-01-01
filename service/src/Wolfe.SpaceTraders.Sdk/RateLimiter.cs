using Microsoft.Extensions.Options;
using System.Net;

namespace Wolfe.SpaceTraders.Sdk;

public class RateLimiter
{
    private readonly SpaceTradersRateLimits _options;
    private readonly SemaphoreSlim _semaphore;
    private uint _backlog;

    public RateLimiter(IOptions<SpaceTradersOptions> options)
    {
        _options = options.Value.RateLimits;
        _semaphore = new SemaphoreSlim(_options.RequestsPerInterval, _options.RequestsPerInterval);
    }

    public async Task<HttpResponseMessage> Limit(Func<Task<HttpResponseMessage>> action, CancellationToken cancellationToken)
    {
        try
        {
            if (_backlog >= _options.MaxQueueLength)
            {
                throw new SpaceTradersApiException("Request queue length exceed.", -1, HttpStatusCode.TooManyRequests);
            }
            Interlocked.Increment(ref _backlog);

            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                return await action();
            }
            finally
            {
                Release();
            }
        }
        finally
        {
            Interlocked.Decrement(ref _backlog);
        }
    }

    private void Release()
    {
        Task
            .Delay(_options.Interval, CancellationToken.None)
            .ContinueWith(_ => _semaphore.Release(), CancellationToken.None);
    }
}