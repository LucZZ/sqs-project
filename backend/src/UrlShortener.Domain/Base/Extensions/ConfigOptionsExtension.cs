using Microsoft.Extensions.Configuration;

namespace UrlShortener.Domain.Base.Extensions;
public static  class ConfigOptionsExtension {
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class {
        return configuration.GetSection(sectionName).Get<T>() ?? throw new Exception($"Section {sectionName} not found.");
    }
}
