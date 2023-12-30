using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Mapping;

internal static class Systems
{
    public static MongoSystem ToMongo(this StarSystem system) => new()
    {
        Id = system.Id.Value,
        Type = system.Type.Value,
        Location = system.Location.ToMongo(),
    };

    public static StarSystem ToDomain(this MongoSystem system) => new()
    {
        Id = new SystemId(system.Id),
        Type = new SystemType(system.Type),
        Location = system.Location.ToDomain(),
    };
}
