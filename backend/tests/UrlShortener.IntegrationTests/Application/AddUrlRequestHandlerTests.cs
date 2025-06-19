using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using UrlShortener.Application.Url;
using UrlShortener.IntegrationTests.Abstractions;
using Xunit;

namespace UrlShortener.IntegrationTests.Application;
public class AddUrlRequestHandlerTests : BaseIntegrationTest {

    public AddUrlRequestHandlerTests(CustomWebApplicationFactory factory) : base(factory) { }

    [Fact]
    public async Task Test() {
        //Arrange

        var request = new AddUrlRequest("www.google.de", "testuser");

        //Act
        var response = await Sender.Send(request);

        //Assert
        response.IsSuccess.ShouldBeFalse();
    }
}
