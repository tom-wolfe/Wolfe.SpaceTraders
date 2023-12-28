using Cocona;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class MissionCommands(IShipService shipService, IContractService contractService)
{
    public async Task<int> Contract([Argument] ShipId shipId, [Argument] ContractId contractId, CancellationToken cancellationToken = default)
    {
        var ship = await shipService.GetShip(shipId, cancellationToken) ?? throw new Exception($"Ship '{shipId}' not found.");
        var contract = await contractService.GetContract(contractId, cancellationToken) ?? throw new Exception($"Contract '{contractId}' not found.");

        var mission = new ProcurementContractMission(ship, contract);
        await mission.Execute();

        return ExitCodes.Success;
    }
}
