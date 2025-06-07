using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Services;

namespace UrlShortener.Persistence.Database;
public sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IApplicationDbContext {

    public DbSet<Url> Urls { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
        configurationBuilder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("app");
        builder.Entity<Url>()
            .HasOne(u => u.User)
            .WithMany(u => u.Urls)
            .HasForeignKey(u => u.UserId);
    }

    private class UtcDateTimeConverter : ValueConverter<DateTime, DateTime> {
        public UtcDateTimeConverter() : base(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)) { }
    }
}
