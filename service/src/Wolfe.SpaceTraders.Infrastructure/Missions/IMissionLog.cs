using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal interface IMissionLog
{
    void OnStatusChanged(IMission mission, MissionStatus status);
    void OnEvent(IMission mission, FormattableString message);
    void OnError(IMission mission, Exception ex);
}
