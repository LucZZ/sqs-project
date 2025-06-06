using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.Url;

public record AddUrlRequest(string Url, string UserName) : IRequest<Result<UrlResponse>>;

internal class AddUrlRequestHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<AddUrlRequest, Result<UrlResponse>> {
    public async Task<Result<UrlResponse>> Handle(AddUrlRequest request, CancellationToken cancellationToken) {
        //TODO check virus

        var urlExists = await _applicationDbContext.Urls
            .Include(u => u.User)
            .AnyAsync(u => u.OriginalUrl == request.Url && u.User.UserName == request.UserName, cancellationToken: cancellationToken);

        if(urlExists) {
            return Result.Failure<UrlResponse>(Error.UrlAlreadyExists);
        }

        var urlSave = true;

        if(!urlSave) {
            return Result.Failure<UrlResponse>(Error.UrlBlocked);
        }

        var shorted = Guid.NewGuid().ToString("N");

        var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken: cancellationToken);

        if (user is null) {
            return Result.Failure<UrlResponse>(Error.UserNotFound);
        }

        var url = _applicationDbContext.Urls.Add(new Domain.Entities.Url(request.Url, shorted, user));

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Result.Success(new UrlResponse(url.Entity.Id, request.Url, shorted));
    }
}
