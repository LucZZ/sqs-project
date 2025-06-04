using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Domain.Entities;
public class User : IdentityUser<int> {

    public List<Url> Urls { get; set; } = [];

    public User() { }
}
