using Shouldly;
using UrlShortener.Application.Url;
using UrlShortener.Domain.Base.Result;
using UrlShortener.IntegrationTests.Abstractions;
using Xunit;

namespace UrlShortener.IntegrationTests.Application;
public class AddUrlRequestHandlerTests : BaseIntegrationTest {

    public AddUrlRequestHandlerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task AddUrl_ShouldReturnSuccess_WhenUserExistsAndUrlDoesNotExist() {
        //Arrange
        var user = DataSeeder.Users.First();

        var request = new AddUrlRequest("https://www.google.de", user.UserName!, "a", "a");

        //Act
        var response = await Sender.Send(request);

        //Assert
        response.IsSuccess.ShouldBeTrue();
    }

    [Fact]
    public async Task AddUrl_ShouldFail_WhenUrlExist() {
        //Arrange
        var user = DataSeeder.Users.First();

        var setupRequest = new AddUrlRequest("https://www.youtube.com", user.UserName!, "a", "a");
        var setupResponse = await Sender.Send(setupRequest);

        //Act
        var response = await Sender.Send(setupRequest);

        //Assert
        setupResponse.IsSuccess.ShouldBeTrue();
        response.IsSuccess.ShouldBeFalse();
        response.Errors.ShouldContain(Error.UrlAlreadyExists);
    }

    [Fact]
    public async Task AddUrl_ShouldFail_WhenVirusTotalApiReturnsSuspicious() {
        //Arrange
        var user = DataSeeder.Users.First();

        var request = new AddUrlRequest("https://www.bad.de", user.UserName!, "a", "a");

        //Act
        var response = await Sender.Send(request);

        //Assert
        response.IsSuccess.ShouldBeFalse();
        response.Errors.ShouldContain(Error.VirusTotalSuspicious);
    }
}
