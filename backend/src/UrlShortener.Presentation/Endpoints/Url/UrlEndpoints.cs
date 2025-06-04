using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace UrlShortener.Presentation.Endpoints.Url;
public class UrlEndpoints : CarterModule {

    public UrlEndpoints() : base("/api/urls"){
        
    }

    public override void AddRoutes(IEndpointRouteBuilder app) {
        app.MapGet("", () => Results.Ok());
        app.MapPost("", () => Results.Ok());
        app.MapPatch("/{Id}", (Guid Id) => Results.Ok());
        app.MapDelete("/{Id}", (Guid Id) => Results.Ok());
    }
}
