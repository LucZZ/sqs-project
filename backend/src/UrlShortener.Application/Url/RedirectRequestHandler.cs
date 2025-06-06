using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.Url;

public record RedirectRequest(string ShortUrl, string UserName) : IRequest<Result<string>>;

internal class RedirectRequestHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<RedirectRequest, Result<string>> {
    public async Task<Result<string>> Handle(RedirectRequest request, CancellationToken cancellationToken) {

        var url = await _applicationDbContext.Urls
            .Include(u => u.User)
            .SingleOrDefaultAsync(u => u.ShortUrl == request.ShortUrl && u.User.UserName == request.UserName, cancellationToken: cancellationToken);

        if(url is null) {
            return Result.Failure<string>(Error.UrlNotFound);
        }

        return Result.Success(url.OriginalUrl);
    }
}
