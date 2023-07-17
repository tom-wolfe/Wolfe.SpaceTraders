using Refit;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class ApiResponseExtensions
{
    public static void EnsureSuccessStatusCode<T>(this IApiResponse<T> response)
    {
        if (response.Error?.InnerException != null)
        {
            throw response.Error.InnerException;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error calling endpoint: {response.StatusCode}");
        }
    }
}