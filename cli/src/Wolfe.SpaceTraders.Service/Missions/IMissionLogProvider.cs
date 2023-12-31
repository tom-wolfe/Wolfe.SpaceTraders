using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

public interface IMissionLogProvider
{
    public IMissionLog CreateLog(MissionId missionId);
}
