using ItemsShop.Common.Domain.Results;
using Microsoft.AspNetCore.Http;

namespace ItemsShop.Common.Api.Extensions;

public static class EndpointResultExtensions
{
    public static int ToStatusCode(this ErrorType error)
    {
        return error switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,

            _ => StatusCodes.Status500InternalServerError
        };
    }
}
