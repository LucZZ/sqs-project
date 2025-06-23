using Refit;
using System.Net;
using UrlShortener.Infrastructure.DTOs;
using UrlShortener.Infrastructure.Services;

namespace UrlShortener.IntegrationTests.Abstractions;
internal class VirusTotalApiFake : IVirusTotalApi {
    public Task<ApiResponse<ReportResponse>> GetReport(string id) {

        ReportResponse response;

        if(id.Contains("bad")) {
            response = new ReportResponse(new ReportData(id, new ReportStats(1, 0, 10, 20, 0)));
        } else {
            response = new ReportResponse(new ReportData(id, new ReportStats(0, 0, 10, 20, 0)));
        }

        return Task.FromResult(new ApiResponse<ReportResponse>(new HttpResponseMessage(HttpStatusCode.OK), response, new RefitSettings()));
    }

    public Task<ApiResponse<ScanResponse>> ScanUrl([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data) {

        if(data.Count != 1 && data.First().Key != "url" && data.First().Value is string) {
            return Task.FromResult(new ApiResponse<ScanResponse>(new HttpResponseMessage(HttpStatusCode.BadRequest), null, new RefitSettings()));
        }

        var url = data.First().Value as string;

        var response = new ScanResponse(new ScanData(url ?? ""));

        return Task.FromResult(new ApiResponse<ScanResponse>(new HttpResponseMessage(HttpStatusCode.OK), response, new RefitSettings()));
    }
}
