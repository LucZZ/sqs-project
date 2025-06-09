using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using UrlShortener.Application.Url;
using UrlShortener.Domain.DTOs.Input;
using UrlShortener.Presentation.Extensions;

namespace UrlShortener.Presentation.Endpoints.Url;
public class UrlEndpoints : CarterModule {

    public UrlEndpoints() : base("/api/urls") {
        base.RequireAuthorization();
    }

    public override void AddRoutes(IEndpointRouteBuilder app) {
        app.MapPost("", async (UrlRequest urlRequest, ClaimsPrincipal claimsPrincipal, ISender sender) => {
            var result = await sender.Send(new AddUrlRequest(urlRequest.Url, claimsPrincipal.Identity?.Name ?? ""));
            return result.ToIResult();
        });
        app.MapGet("", async (ClaimsPrincipal claimsPrincipal, ISender sender) => {
            var result = await sender.Send(new GetUrlList(claimsPrincipal.Identity?.Name ?? ""));
            return result.ToIResult();
        });
        app.MapGet("/{shortUrl}", async (string shortUrl, ClaimsPrincipal claimsPrincipal, ISender sender) => {
            var result = await sender.Send(new RedirectRequest(shortUrl, claimsPrincipal.Identity?.Name ?? ""));
            if(result.IsFailure) {
                result.ToIResult();
            }
            return Results.Redirect(result.Value!);
        }).AllowAnonymous();
    }
}
