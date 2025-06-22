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

    [Fact]
    public void Should_Compare_Null_Errors_As_Equal() {
        // Arrange
        Error? error1 = null;
        Error? error2 = null;

        // Act & Assert
        (error1 == error2).ShouldBeTrue();
        (error1 != error2).ShouldBeFalse();
    }

    [Fact]
    public void Should_Compare_Null_And_NonNull_Errors_As_NotEqual() {
        // Arrange
        Error? error1 = Error.UserAlreadyExists;
        Error? error2 = null;

        // Act & Assert
        (error1 == error2).ShouldBeFalse();
        (error1 != error2).ShouldBeTrue();
        (error2 == error1).ShouldBeFalse(); // Check commutativity
        (error2 != error1).ShouldBeTrue();
    }

    [Fact]
    public void Should_Not_Equal_When_Other_Is_Null() {
        // Arrange
        var error = Error.UserAlreadyExists;

        // Act
        var result = error.Equals((Error?)null);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void Should_Not_Equal_NonError_Object() {
        // Arrange
        var error = Error.UserAlreadyExists;

        // Act
        var result = error.Equals("some string");

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void Should_Have_Same_HashCode_For_Same_Error() {
        // Arrange
        var error1 = Error.UserAlreadyExists;
        var error2 = Error.UserAlreadyExists;

        // Act
        var hash1 = error1.GetHashCode();
        var hash2 = error2.GetHashCode();

        // Assert
        hash1.ShouldBe(hash2);
    }

    [Fact]
    public void Should_Have_Different_HashCode_For_Different_Errors() {
        // Arrange
        var error1 = Error.UserAlreadyExists;
        var error2 = Error.UserNotFound;

        // Act
        var hash1 = error1.GetHashCode();
        var hash2 = error2.GetHashCode();

        // Assert
        hash1.ShouldNotBe(hash2);
    }
}
