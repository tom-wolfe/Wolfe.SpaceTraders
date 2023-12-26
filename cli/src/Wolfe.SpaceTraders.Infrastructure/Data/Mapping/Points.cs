using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data.Mapping;

internal static class Points
{
    public static DataPoint ToData(this Point point) => new(point.X, point.Y);
    public static Point ToDomain(this DataPoint point) => new(point.X, point.Y);
}
