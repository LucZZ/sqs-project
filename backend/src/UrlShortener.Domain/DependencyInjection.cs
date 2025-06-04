using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UrlShortener.Domain;
public static class DependencyInjection {
    public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration) {

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
