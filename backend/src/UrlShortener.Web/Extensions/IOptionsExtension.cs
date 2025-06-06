namespace UrlShortener.Web.Extensions;

public static class IOptionsExtension {
    public static void AddAndValidateOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName) where T : class => services.AddOptions<T>().Bind(configuration.GetSection(sectionName)).ValidateDataAnnotations().ValidateOnStart();
}
