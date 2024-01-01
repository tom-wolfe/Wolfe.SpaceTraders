using MongoDB.Driver;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo;

internal static class MongoHelpers
{
    public static readonly ReplaceOptions InsertOrUpdate = new() { IsUpsert = true };
}
