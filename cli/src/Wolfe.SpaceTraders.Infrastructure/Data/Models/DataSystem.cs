namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataSystem
{
    public required string Id { get; init; }
    public required string Type { get; init; }
    public required DataPoint Location { get; init; }
}
