namespace Wolfe.SpaceTraders.Domain.Missions;

public interface IMissionLog
{
    public ValueTask Write(FormattableString message, CancellationToken cancellationToken = default);
}
