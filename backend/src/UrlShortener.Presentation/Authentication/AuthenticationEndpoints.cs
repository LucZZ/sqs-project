using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace UrlShortener.Presentation.Authentication;
public class AuthenticationEndpoints : CarterModule {

    public AuthenticationEndpoints(): base("/api/auth") {
        
    }

    public override void AddRoutes(IEndpointRouteBuilder app) {
        app.MapPost("/register", () => Results.Ok());
        app.MapPost("/login", () => Results.Ok());
    }
}
