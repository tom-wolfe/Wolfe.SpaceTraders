namespace Wolfe.SpaceTraders.Domain.Systems;

[StronglyTypedId]
public partial struct SystemSymbol
{
    public string Sector => Value.Split('-')[0];
    public string System => Value.Split('-')[1];
}