using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Infrastructure.Mongo;
using Wolfe.SpaceTraders.Service.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MongoMissionLogProvider(IOptions<MongoOptions> options, IMongoClient client) : IMissionLogProvider
{
    public IMissionLog CreateLog(MissionId missionId) => new MongoMissionLog(options, client, missionId);
}
