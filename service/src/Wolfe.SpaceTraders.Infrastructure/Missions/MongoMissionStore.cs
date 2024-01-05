using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MongoMissionStore : IMissionStore
{
    private readonly IMongoCollection<MongoMission> _missionsCollection;

    public MongoMissionStore(IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _missionsCollection = database.GetCollection<MongoMission>(mongoOptions.Value.MissionsCollection);
    }

    public Task UpdateMission(IMission mission, CancellationToken cancellationToken = default)
    {
        var mongoMission = mission.ToMongo();
        return _missionsCollection.ReplaceOneAsync(x => x.Id == mongoMission.Id, mongoMission, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public async Task<IMission?> GetMission(MissionId missionId, IMissionFactory factory, CancellationToken cancellationToken = default)
    {
        var results = await _missionsCollection.FindAsync(m => m.Id == missionId.Value, cancellationToken: cancellationToken);
        var mongoMission = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoMission == null ? null : await factory.Rehydrate(mongoMission, cancellationToken);
    }

    public async IAsyncEnumerable<IMission> GetMissions(IMissionFactory factory, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = await _missionsCollection.FindAsync(_ => true, cancellationToken: cancellationToken);
        var results = query.ToAsyncEnumerable(cancellationToken: cancellationToken);
        await foreach (var result in results)
        {
            yield return await factory.Rehydrate(result, cancellationToken);
        }
    }
}
