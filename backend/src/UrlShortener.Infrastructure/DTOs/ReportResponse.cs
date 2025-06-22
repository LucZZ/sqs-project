namespace UrlShortener.Infrastructure.DTOs;
public record ReportResponse(ReportData Data);

public record ReportData(string Id, ReportStats Stats);

public record ReportStats(int Malicious, int Suspicious, int Undetected, int Harmless, int Timeout);
