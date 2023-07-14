namespace Wolfe.SpaceTraders.Token;

internal interface ITokenReader
{
    Task<string?> Read(CancellationToken cancellationToken);
}