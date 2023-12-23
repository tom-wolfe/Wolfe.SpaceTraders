namespace Wolfe.SpaceTraders.Infrastructure.Token;

public interface ITokenService
{
    Task<string?> Read(CancellationToken cancellationToken);
    Task Write(string token, CancellationToken cancellationToken);
    Task Clear(CancellationToken cancellationToken);
}