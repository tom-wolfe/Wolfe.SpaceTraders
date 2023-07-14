namespace Wolfe.SpaceTraders.Token;

internal interface ITokenGetService
{
    Task<string?> GetToken(CancellationToken cancellationToken);
}