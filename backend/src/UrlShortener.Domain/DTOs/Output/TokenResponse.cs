namespace UrlShortener.Domain.DTOs.Output;
public record TokenResponse(string AccessToken, int ExpiresInSeconds);
