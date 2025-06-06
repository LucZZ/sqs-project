namespace UrlShortener.Infrastructure.DTOs;
public record ScanResponse(ScanData data);

public record ScanData(string type, string id, ScanLinks links);

public record ScanLinks(string self);
