using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Base.Options;
public class JwtOptions {

    public const string SectionName = "JWTOptions";

    [Required]
    public required string JWTSecret { get; set; }
    [Required]
    public required string ValidIssuer { get; set; }
    [Required]
    public required string ValidAudience { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public required int AccessTokenExpirationInMinutes { get; set; }
}
