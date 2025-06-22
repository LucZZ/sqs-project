using Microsoft.AspNetCore.Http.HttpResults;
using Shouldly;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Presentation.Extensions;


namespace UrlShortener.UnitTests.Presentation;
public class ResultExtensionsTests {

    [Fact]
    public void ToIResult_SuccessResult_ReturnsOkResult() {
        // Arrange
        var result = Result.Success();

        // Act
        var httpResult = result.ToIResult();

        // Assert
        httpResult.ShouldBeOfType<Ok>();
    }

    [Fact]
    public void ToIResult_FailureResult_ReturnsJsonWithMaxStatusCode() {
        // Arrange
        var error1 = Error.LoginFailed;  // 409
        var error2 = Error.UrlBlocked;   // 403
        var result = Result.Failure([error1, error2]);

        // Act
        var httpResult = result.ToIResult();

        // Assert
        httpResult.ShouldBeOfType<JsonHttpResult<Result>>();
        var jsonResult = httpResult as JsonHttpResult<Result>;
        jsonResult!.StatusCode.ShouldBe(409);
        jsonResult.Value.ShouldBe(result);
    }

    [Fact]
    public void ToIResult_GenericSuccessResult_ReturnsOkWithWrappedResult() {
        // Arrange
        var result = Result.Success("test-value");

        // Act
        var httpResult = result.ToIResult();

        // Assert
        httpResult.ShouldBeOfType<Ok<Result<string>>>();
        var okResult = httpResult as Ok<Result<string>>;
        okResult!.Value!.Value.ShouldBe("test-value");
    }


    [Fact]
    public void ToIResult_GenericFailureResult_ReturnsJsonWithMaxStatusCode() {
        // Arrange
        var result = Result.Failure<string>([Error.UserNotFound, Error.RegistrationFailed]); // 404, 500

        // Act
        var httpResult = result.ToIResult();

        // Assert
        httpResult.ShouldBeOfType<JsonHttpResult<Result<string>>>();
        var jsonResult = httpResult as JsonHttpResult<Result<string>>;
        jsonResult!.StatusCode.ShouldBe(500);
        jsonResult.Value.ShouldBe(result);
    }
}

