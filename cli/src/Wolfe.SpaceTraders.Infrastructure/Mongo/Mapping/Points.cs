using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Mapping;

internal static class Points
{
    public static MongoPoint ToMongo(this Point point) => new(point.X, point.Y);
    public static Point ToDomain(this MongoPoint point) => new(point.X, point.Y);
}
