namespace Wolfe.SpaceTraders.Domain.Navigation;

[StronglyTypedId]
public partial struct FlightSpeed
{
    public static FlightSpeed Drift => new("DRIFT");
    public static FlightSpeed Cruise => new("CRUISE");
    public static FlightSpeed Burn => new("BURN");
    public static FlightSpeed Stealth => new("STEALTH");

}