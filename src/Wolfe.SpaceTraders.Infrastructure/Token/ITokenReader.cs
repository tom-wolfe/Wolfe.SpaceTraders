namespace Wolfe.SpaceTraders.Infrastructure.Token;

public interface ITokenReader
{
    Task<string?> Read(CancellationToken cancellationToken);
}