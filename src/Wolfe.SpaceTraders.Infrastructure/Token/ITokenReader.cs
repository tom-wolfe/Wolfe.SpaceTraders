namespace Wolfe.SpaceTraders.Infrastructure.Token;

internal interface ITokenReader
{
    Task<string?> Read(CancellationToken cancellationToken);
}