namespace Wolfe.SpaceTraders.Core.Models;

[StronglyTypedId]
public partial struct WaypointSymbol
{
    private string SectorString => Value.Split('-')[0];
    private string SystemString => Value.Split('-')[1];
    public SystemSymbol System => new($"{SectorString}-{SystemString}");
}