namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataMarketplace : DataWaypoint
{
    public required List<DataMarketplaceItem> Imports { get; init; } = [];
    public required List<DataMarketplaceItem> Exports { get; init; } = [];
    public required List<DataMarketplaceItem> Exchange { get; init; } = [];
}
