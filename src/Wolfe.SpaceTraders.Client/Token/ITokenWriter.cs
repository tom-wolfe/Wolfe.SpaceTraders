namespace Wolfe.SpaceTraders.Token;

public interface ITokenWriter
{
    Task Write(string token, CancellationToken cancellationToken);
    Task Clear(CancellationToken cancellationToken);
}