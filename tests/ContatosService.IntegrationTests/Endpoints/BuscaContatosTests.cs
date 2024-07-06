using System.Text.Json;
using ContatosService.IntegrationTests.ApiResponses;

namespace ContatosService.IntegrationTests.Endpoints;

public class BuscaContatosTests(ContatosServiceFactory factory) : Infra.IntegrationTests(factory)
{
    [Theory(DisplayName = "Busca contatos por DDD com sucesso")]
    [InlineData("11")]
    [InlineData("21")]
    public async Task EditarContato_DeveRetornarComSucesso_QuandoParametrosValidos(string ddd)
    {
        // Act
        var response = await Client.GetAsync($"/api/v1/contatos?ddd={ddd}");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var contatos = JsonSerializer.Deserialize<List<Contato>>(content);
        contatos.Should().NotBeNullOrEmpty();
        contatos.Should().HaveCount(3);
        contatos.Should().Contain(c => c.regiao.ddd == ddd);
    }
}