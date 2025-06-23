using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.Url;

public record RedirectRequest(string Code) : IRequest<Result<string>>;

internal class RedirectRequestHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<RedirectRequest, Result<string>> {
    public async Task<Result<string>> Handle(RedirectRequest request, CancellationToken cancellationToken) {

        var url = await _applicationDbContext.Urls
            .SingleOrDefaultAsync(u => u.Code == request.Code, cancellationToken: cancellationToken);

        if(url is null) {
            return Result.Failure<string>(Error.UrlNotFound);
        }

        return Result.Success(url.OriginalUrl);
    }
}
