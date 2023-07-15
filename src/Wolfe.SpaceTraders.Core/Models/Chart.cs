namespace Wolfe.SpaceTraders.Core.Models;

public class Chart
{
    // public required WaypointSymbol WaypointSymbol { get; set; }
    public required AgentSymbol SubmittedBy { get; set; }
    public required DateTimeOffset SubmittedOn { get; set; }
}