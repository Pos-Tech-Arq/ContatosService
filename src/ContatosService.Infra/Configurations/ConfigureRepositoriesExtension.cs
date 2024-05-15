using ContatosService.Domain.Contracts;
using ContatosService.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContatosService.Infra.Configurations;

public static class ConfigureRepositoriesExtension
{
    public static void ConfigureRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IContatosRepository, ContatosRepository>();
        serviceCollection.AddScoped<IRegiaoRepository, RegiaoRepository>();
    }
}