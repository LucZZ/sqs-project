using Shouldly;
using UrlShortener.Application.Url;
using UrlShortener.IntegrationTests.Abstractions;
using Xunit;

namespace UrlShortener.IntegrationTests.Application;
public class GetUrlListRequestHandler : BaseIntegrationTest {



    public GetUrlListRequestHandler(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task GetUrl_ShouldSucceed_WhenUrlInDatabase() {
        //Arrange
        var user = DataSeeder.Users.First();

        var request = new GetUrlListRequest(user.UserName);

        //Act
        var response = await Sender.Send(request);

        //Assert
        response.IsSuccess.ShouldBeTrue();
        response.Value.ShouldNotBeNull();
        response.Value.Count.ShouldBe(2);
    }
}
