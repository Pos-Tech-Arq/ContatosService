using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace ContatosService.IntegrationTests.Infra;

public class DockerFixture
{
    public IContainer MsSqlContainer { get; }

    public DockerFixture()
    {
        MsSqlContainer = new ContainerBuilder()
            .WithCleanUp(true)
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPortBinding(1433, true)
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithEnvironment("SQLCMDUSER", "sa")
            .WithEnvironment("SQLCMDPASSWORD", "Strong_password_123!")
            .WithEnvironment("MSSQL_SA_PASSWORD", "Strong_password_123!")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
            .WithAutoRemove(true)
            .Build();
    }
    
    public Task InitializeAsync() => MsSqlContainer.StartAsync();
    public Task DisposeAsync() => MsSqlContainer.DisposeAsync().AsTask();
}