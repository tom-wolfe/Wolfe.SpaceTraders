namespace Wolfe.SpaceTraders.Domain.Navigation;

[StronglyTypedId]
public partial struct ShipSpeed
{
    public static ShipSpeed Drift => new("DRIFT");
    public static ShipSpeed Cruise => new("CRUISE");
    public static ShipSpeed Burn => new("BURN");
    public static ShipSpeed Stealth => new("STEALTH");

}