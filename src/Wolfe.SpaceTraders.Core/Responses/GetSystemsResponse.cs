using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Responses;

public class GetSystemsResponse
{
    public required IReadOnlyCollection<ShallowStarSystem> Systems { get; init; }
    public required int Remaining { get; init; }
}