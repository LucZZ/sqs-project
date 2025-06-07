using Shouldly;
using UrlShortener.Domain.Base.Result;

namespace UrlShortener.UnitTests.Domain;
public class ErrorTests {

    [Fact]
    public void Should_Equal_Identical_Errors() {
        // Arrange
        var error1 = Error.UserAlreadyExists;
        var error2 = Error.UserAlreadyExists;

        // Act
        var result = error1.Equals(error2);

        // Assert
        result.ShouldBeTrue();
        (error1 == error2).ShouldBeTrue();
        (error1 != error2).ShouldBeFalse();
    }

    [Fact]
    public void Should_Not_Equal_Different_Errors() {
        // Arrange
        var error1 = Error.UserAlreadyExists;
        var error2 = Error.UserNotFound;

        // Act
        var result = error1.Equals(error2);

        // Assert
        result.ShouldBeFalse();
        (error1 == error2).ShouldBeFalse();
        (error1 != error2).ShouldBeTrue();
    }

    [Fact]
    public void Should_Implicitly_Convert_Error_To_String() {
        // Arrange
        var error = Error.UserAlreadyExists;

        // Act
        string code = error;

        // Assert
        code.ShouldBe("Error.UserAlreadyExists");
    }

    [Fact]
    public void Should_Return_Correct_ToString_Value() {
        // Arrange
        var error = Error.UserAlreadyExists;

        // Act
        var result = error.ToString();

        // Assert
        result.ShouldBe("Error.UserAlreadyExists: The user already exisit!");
    }
}
