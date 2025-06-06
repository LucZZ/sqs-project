using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Domain.Base.Options;
public class VirusTotalOptions {

    public const string SectionName = "VirusTotal";

    [Required]
    public string Url { get; set; }

    [Required]
    public string ApiKey { get; set; }

}
