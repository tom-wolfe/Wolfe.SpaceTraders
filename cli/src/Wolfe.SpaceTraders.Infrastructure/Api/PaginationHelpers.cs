using Wolfe.SpaceTraders.Sdk.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class PaginationHelpers
{
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(Func<int, Task<ISpaceTradersListResponse<T>>> getPage)
    {
        var page = 1;
        var count = 0;
        int total;

        do
        {
            var result = await getPage(page);

            count += result.Meta.Limit;
            total = result.Meta.Total;
            page++;

            foreach (var item in result.Data)
            {
                yield return item;
            }
        }
        while (count < total);
    }
}
