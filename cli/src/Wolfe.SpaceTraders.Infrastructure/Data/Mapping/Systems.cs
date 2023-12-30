using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Systems
{
    public static DataSystem ToData(this StarSystem system) => new()
    {
        Id = system.Id.Value,
        Type = system.Type.Value,
        Location = system.Location.ToData(),
    };

    public static StarSystem ToDomain(this DataSystem system) => new()
    {
        Id = new SystemId(system.Id),
        Type = new SystemType(system.Type),
        Location = system.Location.ToDomain(),
    };
}
