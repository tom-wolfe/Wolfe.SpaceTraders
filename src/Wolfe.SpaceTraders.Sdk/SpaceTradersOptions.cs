namespace Wolfe.SpaceTraders.Sdk;

public class SpaceTradersOptions
{
    public required Uri ApiBaseUri { get; init; }
    public required Func<HttpRequestMessage, CancellationToken, Task<string>>? ApiKeyProvider { get; init; }
}