using UrlShortener.Domain.Base.Result;
using UrlShortener.Domain.Services;
using Refit;
using UrlShortener.Infrastructure.DTOs;

namespace UrlShortener.Infrastructure.Services;
internal class VirusTotalService(IVirusTotalApi _virusTotalApi) : IVirusTotalService {
    public async Task<Result> CheckUrl(string url) {

        var scanResult = await _virusTotalApi.ScanUrl(new Dictionary<string, object> { { "url", url } });

        if(!scanResult.IsSuccessful) {
            return Result.Failure(Error.VirusTotalFailed);
        }

        var reportResult = await _virusTotalApi.GetReport(scanResult.Content.Data.Id);

        if (!reportResult.IsSuccessful) {
            return Result.Failure(Error.VirusTotalFailed);
        }

        if(reportResult.Content.Data.Stats is { Malicious: > 0 } or { Suspicious: > 0 }) {
            return Result.Failure(Error.VirusTotalSuspicious);
        }
        return Result.Success();
    }
}

public interface IVirusTotalApi {
    [Post("/api/v3/urls")]
    public Task<ApiResponse<ScanResponse>> ScanUrl([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

    [Get("/api/v3/analyses/{id}")]
    public Task<ApiResponse<ReportResponse>> GetReport(string id);
}

