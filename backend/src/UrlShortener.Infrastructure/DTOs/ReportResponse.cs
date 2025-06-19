namespace UrlShortener.Infrastructure.DTOs;
public record ReportResponse(ReportData Data, ReportMeta Meta);

public record ReportData(string Id, string Type, ReportLinks Links, ReportAttributes Attributes, ReportStats Stats);

public record ReportLinks(string Self, string Item);

public record ReportAttributes(long Date, string Status, Dictionary<string, ReportResult> Results);

public record ReportResult(string Method, string Engine_name, string Category, string Result);

public record ReportStats(int Malicious, int Suspicious, int Undetected, int Harmless, int Timeout);

public record ReportMeta(ReportUrlInfo Url_info);

public record ReportUrlInfo(string Id, string Url);