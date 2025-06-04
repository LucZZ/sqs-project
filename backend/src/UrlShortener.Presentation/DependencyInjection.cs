using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Carter;

namespace UrlShortener.Presentation;
public static class DependencyInjection {
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration) {
        return services;
    }
}
