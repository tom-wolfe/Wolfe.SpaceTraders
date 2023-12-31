namespace Wolfe.SpaceTraders.Domain.Missions;

public interface IMissionLog
{
    public void Write(FormattableString message);
}
