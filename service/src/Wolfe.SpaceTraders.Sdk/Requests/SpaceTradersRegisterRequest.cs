namespace Wolfe.SpaceTraders.Sdk.Requests;

public class SpaceTradersRegisterRequest
{
    public required string Faction { get; set; }
    public required string Symbol { get; set; }
    public string? Email { get; set; }
}