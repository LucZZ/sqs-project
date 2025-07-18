﻿using Carter;
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
        app.MapPost("", async (UrlRequest urlRequest, ClaimsPrincipal claimsPrincipal, ISender sender, HttpContext httpContext) => {
            var result = await sender.Send(new AddUrlRequest(urlRequest.Url, claimsPrincipal.Identity?.Name ?? "", httpContext.Request.Scheme, httpContext.Request.Host.Value ?? ""));
            return result.ToIResult();
        });
        app.MapGet("", async (ClaimsPrincipal claimsPrincipal, ISender sender) => {
            var result = await sender.Send(new GetUrlListRequest(claimsPrincipal.Identity?.Name ?? ""));
            return result.ToIResult();
        });
        app.MapGet("/{code}", async (string code, ISender sender) => {
            var result = await sender.Send(new RedirectRequest(code));
            if(result.IsFailure) {
                result.ToIResult();
            }
            return Results.Redirect(result.Value!);
        }).AllowAnonymous();
    }
}
