using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.DTOs.Output;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.Url;

public record GetUrlListRequest(string UserName) : IRequest<Result<List<UrlResponse>>>;

internal class GetUrlListRequestHandler(IApplicationDbContext _applicationDbContext) : IRequestHandler<GetUrlListRequest, Result<List<UrlResponse>>> {
    public async Task<Result<List<UrlResponse>>> Handle(GetUrlListRequest request, CancellationToken cancellationToken) {
        var result = await _applicationDbContext.Urls
            .Where(u => u.User.UserName == request.UserName)
            .Select(u => new UrlResponse(u.Id, u.OriginalUrl, u.ShortUrl))
            .ToListAsync(cancellationToken);

        return Result.Success(result);
    }
}
