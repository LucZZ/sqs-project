using System.Collections.Specialized;
using UrlShortener.Domain.Base.Entity;

namespace UrlShortener.Domain.Entities;
public class Url : EntityBase {

    public string OriginalUrl { get; set; }

    public string ShortUrl { get; set; }

    public string Code { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public Url(string originalUrl, string shortUrl, string code, User user) {
        OriginalUrl = originalUrl;
        ShortUrl = shortUrl;
        User = user;
        Code = code;
    }

    private Url() { }
}
