namespace Wolfe.SpaceTraders.Infrastructure.Missions.Models;

internal class MongoMissionLogError
{
    public required string Type { get; init; }
    public required string Message { get; set; }
    public required string StackTrace { get; set; }
}