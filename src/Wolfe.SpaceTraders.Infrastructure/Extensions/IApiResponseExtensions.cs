using Refit;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class ApiResponseExtensions
{
    public static T GetContent<T>(this IApiResponse<T> response)
    {
        if (response.Error?.InnerException != null)
        {
            throw response.Error.InnerException;
        }

        if (response.Error != null)
        {
            throw response.Error;
        }

        if (response.Content == null)
        {
            throw new InvalidOperationException("Response content is null");
        }

        return response.Content;
    }
}