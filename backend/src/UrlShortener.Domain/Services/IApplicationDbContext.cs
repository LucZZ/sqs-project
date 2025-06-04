using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Services;
public interface IApplicationDbContext {
    public DbSet<User> Users { get; set; }

    public DbSet<Url> Urls { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellation = default);
}
