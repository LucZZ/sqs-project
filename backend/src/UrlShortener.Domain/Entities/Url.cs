using UrlShortener.Domain.Base.Entity;

namespace UrlShortener.Domain.Entities;
public class Url : EntityBase {

    public string OriginalUrl { get; set; }

    public string ShortUrl { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public Url(string originalUrl, string shortUrl, User user) {
        OriginalUrl = originalUrl;
        ShortUrl = shortUrl;
        User = user;
    }

    private Url() { }
}
