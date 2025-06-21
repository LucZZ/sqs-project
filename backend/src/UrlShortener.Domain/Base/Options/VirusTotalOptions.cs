using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Base.Options;
public class VirusTotalOptions {

    public const string SectionName = "VirusTotal";

    [Required]
    public required string Url { get; set; }

    [Required]
    public required string ApiKey { get; set; }

}
