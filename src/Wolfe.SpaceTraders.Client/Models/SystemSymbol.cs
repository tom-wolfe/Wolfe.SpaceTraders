namespace Wolfe.SpaceTraders.Models;

[StronglyTypedId]
public partial struct SystemSymbol
{
    public string Sector => Value.Split('-')[0];
    public string System => Value.Split('-')[1];
}