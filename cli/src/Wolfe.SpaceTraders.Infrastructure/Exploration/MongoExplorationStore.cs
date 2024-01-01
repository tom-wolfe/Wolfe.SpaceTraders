using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Infrastructure.Exploration.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;

namespace Wolfe.SpaceTraders.Infrastructure.Exploration;

internal class MongoExplorationStore : IExplorationStore
{
    private readonly IMongoCollection<MongoSystem> _systemsCollection;
    private readonly IMongoCollection<MongoWaypoint> _waypointsCollection;

    public MongoExplorationStore(IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _systemsCollection = database.GetCollection<MongoSystem>(mongoOptions.Value.SystemsCollection);
        _waypointsCollection = database.GetCollection<MongoWaypoint>(mongoOptions.Value.WaypointsCollection);
    }

    public Task AddSystem(StarSystem system, CancellationToken cancellationToken = default)
    {
        var mongoSystem = system.ToMongo();
        return _systemsCollection.ReplaceOneAsync(x => x.Id == mongoSystem.Id, mongoSystem, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default)
    {
        var mongoWaypoint = waypoint.ToMongo();
        return _waypointsCollection.ReplaceOneAsync(x => x.Id == mongoWaypoint.Id, mongoWaypoint, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public async Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken)
    {
        var results = await _systemsCollection.FindAsync(s => s.Id == systemId.Value, cancellationToken: cancellationToken);
        var mongoSystem = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoSystem?.ToDomain();
    }

    public async IAsyncEnumerable<StarSystem> GetSystems([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var query = await _systemsCollection.FindAsync(_ => true, cancellationToken: cancellationToken);
        var results = query.ToAsyncEnumerable(cancellationToken: cancellationToken);
        await foreach (var result in results)
        {
            yield return result.ToDomain();
        }
    }

    public async Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var results = await _waypointsCollection.FindAsync(w => w.Id == waypointId.Value, cancellationToken: cancellationToken);
        var mongoWaypoint = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoWaypoint?.ToDomain();
    }

    public async IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = await _waypointsCollection.FindAsync(w => w.SystemId == systemId.Value, cancellationToken: cancellationToken);
        var results = query.ToAsyncEnumerable(cancellationToken: cancellationToken);
        await foreach (var result in results)
        {
            yield return result.ToDomain();
        }
    }

    public Task Clear(CancellationToken cancellationToken = default) => Task.WhenAll(
        _systemsCollection.DeleteManyAsync(_ => true, cancellationToken),
        _waypointsCollection.DeleteManyAsync(_ => true, cancellationToken)
    );
}