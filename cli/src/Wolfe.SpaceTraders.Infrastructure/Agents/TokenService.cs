using Wolfe.SpaceTraders.Infrastructure.Data;

namespace Wolfe.SpaceTraders.Infrastructure.Agents;

internal class TokenService(ISpaceTradersDataClient dataClient) : ITokenService
{
    public Task SetAccessToken(string token, CancellationToken cancellationToken)
    {
        return dataClient.SetAccessToken(token, cancellationToken);
    }

    public Task<string?> GetAccessToken(CancellationToken cancellationToken)
    {
        return dataClient.GetAccessToken(cancellationToken);
    }
}