using UrlShortener.Domain.Base.Result;

namespace UrlShortener.Domain.Services;
public interface IVirusTotalService {
    public Task<Result> CheckUrl(string url);
}
