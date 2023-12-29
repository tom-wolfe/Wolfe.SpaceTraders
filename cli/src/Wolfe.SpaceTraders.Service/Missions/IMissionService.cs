using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Missions;

public interface IMissionService
{
    public IMission CreateContractMission(Ship ship, Contract contract);
    public IMission CreateProbeMission(Ship ship);
    public IMission CreateTradeMission(Ship ship);
}