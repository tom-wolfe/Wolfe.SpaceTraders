﻿namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

internal class MongoMarketplace : MongoWaypoint
{
    public required IReadOnlyCollection<MongoMarketplaceItem> Imports { get; init; } = [];
    public required IReadOnlyCollection<MongoMarketplaceItem> Exports { get; init; } = [];
    public required IReadOnlyCollection<MongoMarketplaceItem> Exchange { get; init; } = [];
}

internal record MongoMarketplaceItem(string Id, string Name, string Description);