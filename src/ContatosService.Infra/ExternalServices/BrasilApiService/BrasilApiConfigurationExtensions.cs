using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ContatosService.Infra.ExternalServices.BrasilApiService;

public static class BrasilApiConfigurationExtensions
{
    public static void AddBrasilApiClientExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(BrasilApiSettings)).Get<BrasilApiSettings>();

        services.AddRefitClient<IBrasilApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(settings.BaseUrl));
    }
}