using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

public class ProbeMission(IMissionLog log, Ship ship, IExplorationService explorationService) : Mission(log)
{
    public override async Task Execute(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var marketplace = await HighestPriorityMarketplace(cancellationToken);

            await ship.BeginNavigationTo(marketplace.Id, FlightSpeed.Cruise, cancellationToken);


        }
    }

    private ValueTask<Marketplace.Marketplace> HighestPriorityMarketplace(CancellationToken cancellationToken = default) => explorationService
        .GetMarketplaces(ship.Navigation.WaypointId.System, cancellationToken)
        .OrderBy(MarketplacePriority)
        .FirstAsync(cancellationToken);

    private int MarketplacePriority(Marketplace.Marketplace marketplace)
    {
        // TODO: Prioritize by distance and age of last probe.
        return 1;
    }
}
