using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
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
            Message = message.ToString()
        };
        await _missionLogCollection.InsertOneAsync(data, cancellationToken: cancellationToken);
    }
}
