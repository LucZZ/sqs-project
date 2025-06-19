namespace UrlShortener.Infrastructure.DTOs;
public record ScanResponse(ScanData Data);

public record ScanData(string Type, string Id, ScanLinks Links);

public record ScanLinks(string Self);
