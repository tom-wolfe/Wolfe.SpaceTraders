namespace Wolfe.SpaceTraders.Token;

public interface ITokenSetService
{
    Task SetToken(string token, CancellationToken cancellationToken);
}