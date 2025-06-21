using Shouldly;
using UrlShortener.Application.Url;
using UrlShortener.Domain.Base.Result;
using UrlShortener.IntegrationTests.Abstractions;
using Xunit;

namespace UrlShortener.IntegrationTests.Application;
public class RedirectRequestHandlerTests : BaseIntegrationTest {
    public RedirectRequestHandlerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Redirect_ShouldSucceed_WhenUrlIsInDatabase() {
        //Arrange
        var url = DataSeeder.Urls.First();

        var request = new RedirectRequest(url.ShortUrl);

        //Act
        var response = await Sender.Send(request);

        //Assert
        response.IsSuccess.ShouldBeTrue();
        response.Value.ShouldBe(url.OriginalUrl);
    }

    [Fact]
    public async Task Redirect_ShouldFail_WhenUrlIsNotInDatabase() {
        //Arrange
        var request = new RedirectRequest("abc");

        //Act
        var response = await Sender.Send(request);

        //Assert
        response.IsSuccess.ShouldBeFalse();
        response.Errors.ShouldContain(Error.UrlNotFound);
    }

}
