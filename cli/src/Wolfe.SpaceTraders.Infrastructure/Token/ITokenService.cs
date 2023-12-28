namespace Wolfe.SpaceTraders.Infrastructure.Token;

public interface ITokenService
{
    Task<string?> GetAccessToken(CancellationToken cancellationToken);
    Task SetAccessToken(string token, CancellationToken cancellationToken);
}