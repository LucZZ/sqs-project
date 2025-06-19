using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using UrlShortener.Application.Authentication;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.Entities;
using UrlShortener.UnitTests.Abstractions;

namespace UrlShortener.UnitTests.Application.Authentication;
public class RegisterRequestHandlerTests {

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserDoesNotExist_AndCreationSucceeds() {
        // Arrange
        var mockUserManager = MockHelpers.MockUserManager<User>();

        var userName = "testuser";
        var password = "mysecretpassword0";
        var registerRequest = new RegisterRequest(userName, password);

        mockUserManager
            .Setup(um => um.FindByNameAsync(userName))
            .ReturnsAsync((User?)null);

        mockUserManager
            .Setup(um => um.CreateAsync(It.Is<User>(u => u.UserName == userName), password))
            .ReturnsAsync(IdentityResult.Success);

        var handler = new RegisterRequestHandler(mockUserManager.Object);

        // Act
        var result = await handler.Handle(registerRequest, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeTrue();

        // Verify interactions
        mockUserManager.Verify(um => um.FindByNameAsync(userName), Times.Once);
        mockUserManager.Verify(um => um.CreateAsync(It.Is<User>(u => u.UserName == userName), password), Times.Once);
    }


    [Fact]
    public async Task Should_ReturnFailure_When_UserAlreadyExists() {
        // Arrange
        var mockUserManager = MockHelpers.MockUserManager<User>();
        var existingUser = new User { UserName = "testuser" };

        mockUserManager
            .Setup(um => um.FindByNameAsync("testuser"))
            .ReturnsAsync(existingUser);

        var request = new RegisterRequest("testuser", "irrelevant");

        var handler = new RegisterRequestHandler(mockUserManager.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(Error.UserAlreadyExists);
    }

    [Fact]
    public async Task Should_ReturnFailure_When_CreateFails() {
        // Arrange
        var mockUserManager = MockHelpers.MockUserManager<User>();

        mockUserManager
            .Setup(um => um.FindByNameAsync("newuser"))
            .ReturnsAsync((User)null!);

        mockUserManager
            .Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Simulated failure" }));

        var request = new RegisterRequest("newuser", "mypassword");

        var handler = new RegisterRequestHandler(mockUserManager.Object);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(Error.RegistrationFailed);
    }
}
