namespace Wolfe.SpaceTraders.Infrastructure.Token;

public interface ITokenWriter
{
    Task Write(string token, CancellationToken cancellationToken);
    Task Clear(CancellationToken cancellationToken);
}