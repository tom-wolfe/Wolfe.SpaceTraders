namespace Wolfe.SpaceTraders.Infrastructure.Agents;

public interface ITokenService
{
    Task<string?> GetAccessToken(CancellationToken cancellationToken);
    Task SetAccessToken(string token, CancellationToken cancellationToken);
}