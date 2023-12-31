﻿using MongoDB.Bson.Serialization.Attributes;

namespace Wolfe.SpaceTraders.Infrastructure.Agents.Models;

internal class MongoAccessToken
{
    public const string Default = "default";

    [BsonId]
    public string Id => Default;
    public required string Token { get; init; }
}
