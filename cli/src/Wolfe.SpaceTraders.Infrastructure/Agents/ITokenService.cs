namespace Wolfe.SpaceTraders.Infrastructure.Agents;

public interface ITokenService
{
    Task<string?> GetAccessToken(CancellationToken cancellationToken = default);
    Task SetAccessToken(string token, CancellationToken cancellationToken = default);
    Task ClearAccessToken(CancellationToken cancellationToken = default);
}