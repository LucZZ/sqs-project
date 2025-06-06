namespace UrlShortener.Domain.DTOs.Output;
public record UrlResponse(int Id, string OriginalUrl, string ShortUrl);
