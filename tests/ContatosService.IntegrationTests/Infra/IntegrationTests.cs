using ContatosService.Infra.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace ContatosService.IntegrationTests.Infra;

[Collection(name: nameof(ContatosFactoryCollection))]
public abstract class IntegrationTests(ContatosServiceFactory factory)
{
    private readonly AsyncServiceScope _integrationTestScope = factory.Services.CreateAsyncScope();
    protected HttpClient Client => factory.Server.CreateClient();

    protected ApplicationDbContext DbContext =>
        _integrationTestScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}