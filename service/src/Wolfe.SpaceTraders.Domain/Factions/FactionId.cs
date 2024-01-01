namespace Wolfe.SpaceTraders.Domain.Factions;

[StronglyTypedId]
public partial struct FactionId
{
    public static FactionId Cosmic => new("COSMIC");
    public static FactionId Void => new("VOID");
    public static FactionId Galactic => new("GALACTIC");
    public static FactionId Quantum => new("QUANTUM");
    public static FactionId Dominion => new("DOMINION");
    public static FactionId Astro => new("ASTRO");
    public static FactionId Corsairs => new("CORSAIRS");
    public static FactionId Obsidian => new("OBSIDIAN");
    public static FactionId Aegis => new("AEGIS");
    public static FactionId United => new("UNITED");
    public static FactionId Solitary => new("SOLITARY");
    public static FactionId Cobalt => new("COBALT");
    public static FactionId Omega => new("OMEGA");
    public static FactionId Echo => new("ECHO");
    public static FactionId Lords => new("LORDS");
    public static FactionId Cult => new("CULT");
    public static FactionId Ancients => new("ANCIENTS");
    public static FactionId Shadow => new("SHADOW");
    public static FactionId Ethereal => new("ETHEREAL");
}