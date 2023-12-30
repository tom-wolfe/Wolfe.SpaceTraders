using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Extensions;

internal static class AsyncCursorExtensions
{
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IAsyncCursor<T> source, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using (source)
        {
            while (await source.MoveNextAsync(cancellationToken).ConfigureAwait(false))
            {
                foreach (var item in source.Current)
                {
                    yield return item;
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
