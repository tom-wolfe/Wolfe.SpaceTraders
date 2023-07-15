namespace Wolfe.SpaceTraders.Models;

[StronglyTypedId]
public partial struct WaypointSymbol
{
    public string Sector => Value.Split('-')[0];
    public string System => Value.Split('-')[1];
    public string SystemSymbol => $"{Sector}-{System}";
}