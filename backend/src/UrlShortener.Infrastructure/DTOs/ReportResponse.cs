namespace UrlShortener.Infrastructure.DTOs;
public record ReportResponse(ReportData data, ReportMeta meta);

public record ReportData(string id, string type, ReportLinks links, ReportAttributes attributes, ReportStats stats);

public record ReportLinks(string self, string item);

public record ReportAttributes(long date, string status, Dictionary<string, ReportResult> results);

public record ReportResult(string method, string engine_name, string category, string result);

public record ReportStats(int malicious, int suspicious, int undetected, int harmless, int timeout);

public record ReportMeta(ReportUrlInfo url_info);

public record ReportUrlInfo(string id, string url);