using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

public interface IMissionLogFactory
{
    public IMissionLog CreateMissionLog();
}
