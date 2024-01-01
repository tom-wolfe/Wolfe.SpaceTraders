namespace Wolfe.SpaceTraders.Sdk.Models.Extraction;

public class SpaceTradersExtraction
{
    public required string ShipSymbol { get; set; }
    public required SpaceTradersExtractionYield Yield { get; set; }
}