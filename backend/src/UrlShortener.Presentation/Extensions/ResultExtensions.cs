using Microsoft.AspNetCore.Http;
using UrlShortener.Domain.Base.Result;

namespace UrlShortener.Presentation.Extensions;
internal static class ResultExtensions {

    public static IResult ToIResult(this Result result) => result.IsSuccess ? Results.Ok() : Results.Json(result, statusCode: result.Errors.Select(x => x.StatusCode).Max());
 
    public static IResult ToIResult<T>(this Result<T> result) => result.IsSuccess ? Results.Ok(result) : Results.Json(result, statusCode: result.Errors.Select(x => x.StatusCode).Max());
}
