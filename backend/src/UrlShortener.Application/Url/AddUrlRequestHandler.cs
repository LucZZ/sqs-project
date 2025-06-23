using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.Url;

public record AddUrlRequest(string Url, string UserName, string Scheme, string Host) : IRequest<Result<UrlResponse>>;

internal class AddUrlRequestHandler(IApplicationDbContext _applicationDbContext, IVirusTotalService _virusTotalService) : IRequestHandler<AddUrlRequest, Result<UrlResponse>> {
    public async Task<Result<UrlResponse>> Handle(AddUrlRequest request, CancellationToken cancellationToken) {

        if(!Uri.TryCreate(request.Url, UriKind.Absolute, out _)) {
            return Result.Failure<UrlResponse>(Error.UrlInvalid);
        }

        var urlExists = await _applicationDbContext.Urls
            .AnyAsync(u => u.OriginalUrl == request.Url, cancellationToken: cancellationToken);

        if(urlExists) {
            return Result.Failure<UrlResponse>(Error.UrlAlreadyExists);
        }

        var urlSave = await _virusTotalService.CheckUrl(request.Url);

        if(urlSave.IsFailure) {
            return Result.Failure<UrlResponse>(urlSave.Errors);
        }

        var shorted = Guid.NewGuid().ToString("N");

        var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken: cancellationToken);

        if (user is null) {
            return Result.Failure<UrlResponse>(Error.UserNotFound);
        }

        var url = _applicationDbContext.Urls.Add(new Domain.Entities.Url(request.Url, $"{request.Scheme}://{request.Host}/api/urls/{shorted}", shorted, user));

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new UrlResponse(url.Entity.Id, request.Url, shorted));
    }
}
