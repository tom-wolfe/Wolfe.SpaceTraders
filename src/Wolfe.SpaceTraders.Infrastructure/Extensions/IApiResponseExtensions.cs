using Refit;
using Wolfe.SpaceTraders.Infrastructure.Responses;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class ApiResponseExtensions
{
    public static async Task EnsureSuccessStatusCode<T>(this IApiResponse<T> response)
    {
        if (response.Error?.InnerException != null)
        {
            throw response.Error.InnerException;
        }

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Error.GetContentAsAsync<SpaceTradersErrorResponse>();
            throw new SpaceTradersApiException(error!.Error.Message, error.Error.Code, response.StatusCode);
        }
    }
}