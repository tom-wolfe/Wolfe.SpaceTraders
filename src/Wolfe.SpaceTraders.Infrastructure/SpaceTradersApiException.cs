using System.Net;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersApiException(
    string message,
    int errorCode,
    HttpStatusCode statusCode
) : HttpRequestException(
    message: message,
    statusCode: statusCode,
    inner: null
)
{
    public int ErrorCode { get; } = errorCode;
}