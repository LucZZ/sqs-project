using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Base.Options;
public class JWTOptions {

    public const string SectionName = "JWTOptions";

    [Required]
    public string JWTSecret { get; set; }
    [Required]
    public string ValidIssuer { get; set; }
    [Required]
    public string ValidAudience { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int AccessTokenExpirationInMinutes { get; set; }
}
