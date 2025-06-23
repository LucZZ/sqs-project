using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UrlShortener.Domain.Base.Extensions;
using UrlShortener.Domain.Base.Options;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Services;
using UrlShortener.Persistence.Database;

namespace UrlShortener.Persistence;
public static class DependencyInjection {
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {

        var jwtOptions = configuration.GetOptions<JwtOptions>(JwtOptions.SectionName);
        var databaseOptions = configuration.GetOptions<DatabaseOptions>(DatabaseOptions.SectionName);

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseOptions.ConnectionString, sqlOptions => sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

        services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.SaveToken = true;
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters() {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromSeconds(5),

                ValidAudience = jwtOptions.ValidAudience,
                ValidIssuer = jwtOptions.ValidIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JWTSecret))
            };
        });

        services.AddAuthorization();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
