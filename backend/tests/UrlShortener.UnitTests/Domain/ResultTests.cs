using Shouldly;
using UrlShortener.Domain.Base.Result;

namespace UrlShortener.UnitTests.Domain;
public class ResultTests {

    [Fact]
    public void Should_Create_Successful_Result() {
        // Arrange
        var result = Result.Success();

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.Errors.ShouldBe([Error.None]);
    }

    [Fact]
    public void Should_Create_Failure_Result_With_Error() {
        // Arrange
        var error = Error.UserAlreadyExists;
        var result = Result.Failure(error);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Errors.ShouldContain(error);
    }

    [Fact]
    public void Should_Create_Successful_Result_With_Value() {
        // Arrange
        var value = 42;
        var result = Result.Success(value);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(42);
    }

    [Fact]
    public void Should_Create_Failure_Result_With_Value() {
        // Arrange
        var error = Error.UserAlreadyExists;
        var result = Result.Failure<int>(error);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Value.ShouldBe(0);  // Default value of int
        result.Errors.ShouldContain(error);
    }
}
