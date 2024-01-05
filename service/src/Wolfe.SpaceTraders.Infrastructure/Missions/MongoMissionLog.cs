using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MongoMissionLog : IMissionLog
{
    private readonly MissionId _missionId;
    private readonly IMongoCollection<MongoMissionLogData> _missionLogCollection;

    public MongoMissionLog(IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient, MissionId missionId)
    {
        _missionId = missionId;
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _missionLogCollection = database.GetCollection<MongoMissionLogData>(mongoOptions.Value.MissionLogsCollection);
    }

    public async ValueTask Write(FormattableString message, CancellationToken cancellationToken = default)
    {
        var data = new MongoMissionLogData
        {
            Id = ObjectId.GenerateNewId(),
            MissionId = _missionId.Value,
            Message = message.ToString(),
            Template = message.Format,
            Data = message
                .GetArguments()
                .Select((value, index) => (value, index))
                .ToDictionary(v => v.index.ToString(), v => (object?)v.value?.ToString()),
            Timestamp = DateTimeOffset.UtcNow
        };
        await _missionLogCollection.InsertOneAsync(data, cancellationToken: cancellationToken);
    }

    public async ValueTask WriteError(Exception ex, CancellationToken cancellationToken = default)
    {
        var data = new MongoMissionLogData
        {
            Id = ObjectId.GenerateNewId(),
            MissionId = _missionId.Value,
            Template = ex.Message,
            Message = ex.Message,
            Data = ToDictionary(ex.Data),
            Error = new MongoMissionLogError
            {
                Type = ex.GetType().FullName ?? ex.GetType().Name,
                Message = ex.Message,
                StackTrace = ex.StackTrace ?? string.Empty
            },
            Timestamp = DateTimeOffset.UtcNow
        };
        await _missionLogCollection.InsertOneAsync(data, cancellationToken: cancellationToken);
    }

    private static Dictionary<string, object?> ToDictionary(IDictionary source)
    {
        return source.Keys.Cast<object>().ToDictionary(key => key!.ToString() ?? "", key => source[key]);
    }
}
