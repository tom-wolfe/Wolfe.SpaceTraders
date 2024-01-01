using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wolfe.SpaceTraders.Infrastructure.Agents.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;

namespace Wolfe.SpaceTraders.Infrastructure.Agents;

internal class TokenService : ITokenService
{
    private readonly IMongoCollection<MongoAccessToken> _tokensCollection;

    public TokenService(IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _tokensCollection = database.GetCollection<MongoAccessToken>(mongoOptions.Value.TokensCollection);
    }

    public Task SetAccessToken(string token, CancellationToken cancellationToken)
    {
        var mongoToken = new MongoAccessToken { Token = token };
        return _tokensCollection.ReplaceOneAsync(s => s.Id == MongoAccessToken.Default, mongoToken, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public Task ClearAccessToken(CancellationToken cancellationToken = default)
    {
        return _tokensCollection.DeleteOneAsync(s => s.Id == MongoAccessToken.Default, cancellationToken);
    }

    public async Task<string?> GetAccessToken(CancellationToken cancellationToken)
    {
        var results = await _tokensCollection.FindAsync(s => s.Id == MongoAccessToken.Default, cancellationToken: cancellationToken);
        var mongoToken = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoToken?.Token;
    }
}