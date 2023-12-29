using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Missions;

// TODO: Rename this to something cooler.
internal class MissionService(
    IMissionLogFactory logFactory,
    IExplorationService explorationService,
    IWayfinderService wayfinderService
) : IMissionService
{
    public IMission CreateContractMission(Ship ship, Contract contract)
    {
        if (contract.Type == ContractType.Procurement)
        {
            return new ProcurementContractMission(logFactory.CreateMissionLog(), ship, contract);
        }
        throw new NotImplementedException();
    }

    public IMission CreateProbeMission(Ship ship) => new ProbeMission(logFactory.CreateMissionLog(), ship, explorationService);
    public IMission CreateTradeMission(Ship ship) => new TradingMission(logFactory.CreateMissionLog(), ship, wayfinderService);
}