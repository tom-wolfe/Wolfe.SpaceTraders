using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

internal interface IMissionLogFactory
{
    public IMissionLog CreateLog(MissionId missionId);
}
