using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using UrlShortener.Application.Authentication;
using UrlShortener.Domain.Base.Options;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.Entities;
using UrlShortener.UnitTests.Abstractions;

namespace UrlShortener.UnitTests.Application.Authentication;
public class LoginRequestHandlerTests {

    private static (LoginRequestHandler Handler, Mock<UserManager<User>> MockUserManager, LoginRequest Request) CreateHandler(User? userResult, bool passwordCheck, JwtOptions? options = null) {
        var mockUserManager = MockHelpers.MockUserManager<User>();

        mockUserManager
            .Setup(um => um.FindByNameAsync("testuser"))
            .ReturnsAsync(userResult);

        if (userResult is not null) {
            mockUserManager
                .Setup(um => um.CheckPasswordAsync(userResult, "password123"))
                .ReturnsAsync(passwordCheck);
        }

        var timeProvider = Mock.Of<TimeProvider>(tp => tp.GetUtcNow() == new DateTimeOffset(new DateTime(2024, 01, 01, 12, 0, 0, DateTimeKind.Utc)));

        var jwtOptions = Options.Create(options ?? new JwtOptions {
            JWTSecret = "abcdefghijklmnopqrstuvwxyz123456",
            ValidIssuer = "TestIssuer",
            ValidAudience = "TestAudience",
            AccessTokenExpirationInMinutes = 60
        });

        var handler = new LoginRequestHandler(mockUserManager.Object, timeProvider, jwtOptions);
        var request = new LoginRequest("testuser", "password123");

        return (handler, mockUserManager, request);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserNotFound() {
        var (handler, _, request) = CreateHandler(userResult: null, passwordCheck: false);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(Error.LoginFailed);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenPasswordInvalid() {
        var fakeUser = new User { Id = 0, UserName = "testuser" };

        var (handler, _, request) = CreateHandler(userResult: fakeUser, passwordCheck: false);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.ShouldBeFalse();
        result.Errors.ShouldContain(Error.LoginFailed);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenLoginSucceeds() {
        var user = new User { Id = 0, UserName = "testuser" };

        var jwtOptions = new JwtOptions {
            JWTSecret = "supersecuresecretthatworks123!!?!?!?",
            ValidIssuer = "TestIssuer",
            ValidAudience = "TestAudience",
            AccessTokenExpirationInMinutes = 60
        };

        var (handler, _, request) = CreateHandler(userResult: user, passwordCheck: true, options: jwtOptions);

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSuccess.ShouldBeTrue();
        result.Value.AccessToken.ShouldNotBeNullOrWhiteSpace();
        //result.Value.ExpiresInSeconds.ShouldBeInRange(3550, 3599);
    }

}
