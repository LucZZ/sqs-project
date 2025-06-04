using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using UrlShortener.Application.Authentication;
using UrlShortener.Domain.DTOs.Input;
using UrlShortener.Presentation.Extensions;

namespace UrlShortener.Presentation.Endpoints.Authentication;
public class AuthenticationEndpoints : CarterModule {

    public AuthenticationEndpoints(): base("/api/auth") {
        
    }

    public override void AddRoutes(IEndpointRouteBuilder app) {
        app.MapPost("/register", async (UserAuthRequest userAuthRequest, ISender sender) => {
            var result = await sender.Send(new RegisterRequest(userAuthRequest.UserName, userAuthRequest.Password));
            return result.ToIResult();
        });

        app.MapPost("/login", async (UserAuthRequest userAuthRequest, ISender sender) => {
            var result = await sender.Send(new LoginRequest(userAuthRequest.UserName, userAuthRequest.Password));
            result.ToIResult();
        });
    }
}
