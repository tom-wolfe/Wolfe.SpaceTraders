using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class MissionCommands(IShipService shipService, IContractService contractService, IHostApplicationLifetime host)
{
    public async Task<int> Contract([Argument] ShipId shipId, [Argument] ContractId contractId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship '{shipId}' not found.");
        var contract = await contractService.GetContract(contractId, host.ApplicationStopping) ?? throw new Exception($"Contract '{contractId}' not found.");

        // TODO: Make this non-blocking.
        var mission = new ProcurementContractMission(ship, contract);
        await mission.Execute(host.ApplicationStopping);

        return ExitCodes.Success;
    }

    public async Task<int> Trading([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship '{shipId}' not found.");

        // TODO: Make this non-blocking.
        var mission = new TradingMission(ship);
        await mission.Execute(host.ApplicationStopping);

        return ExitCodes.Success;
    }

    // TODO: Starting a mission should add it to a list accessible via a "List" command.
    // TODO: You should be able to get the details of a single mission.
    // TODO: You should be able to request the stopping of a single mission.
}
