using System.Net.Http.Json;
using System.Text.Json;
using ContatosService.Api.Requests;
using ContatosService.IntegrationTests.ApiResponses;
using ContatosService.IntegrationTests.Order;
using Microsoft.EntityFrameworkCore;

namespace ContatosService.IntegrationTests.Endpoints;

[TestCaseOrderer("ContatosService.IntegrationTests.Order.PriorityOrderer", "ContatosService.IntegrationTests")]
public class ContatoTests(ContatosServiceFactory factory) : Infra.IntegrationTests(factory)
{
    [Theory(DisplayName = "Cria novo contato com sucesso"), TestPriority(1)]
    [InlineData("11", "999999999", "Fulano", "fulano@email.com")]
    public async Task CriarContato_DeveRetornarComSucesso_QuandoParametrosValidos(string ddd, string numero,
        string nome, string email)
    {
        // Arrange
        var contatoRquest = new CriaContatoRequest
        {
            Nome = nome,
            Email = email,
            Telefone = new TelefoneRequest
            {
                Ddd = ddd,
                Numero = numero
            }
        };

        // Act
        var postResponse = await Client.PostAsJsonAsync("/api/v1/contatos", contatoRquest);

        // Assert
        postResponse.EnsureSuccessStatusCode();
        var contato = await DbContext.Contatos.FirstOrDefaultAsync(c => c.Email == email);
        contato.Should().NotBeNull();
        contato.Telefone.Ddd.Should().Be(ddd);
        contato.Telefone.Numero.Should().Be(numero);
        contato.Nome.Should().Be(nome);
    }

    [Theory(DisplayName = "Edita contato com sucesso"), TestPriority(2)]
    [InlineData("C1B1DC14-ABAD-483E-87A9-BF46954C8E10", "11", "999999999", "Ciclano Editado",
        "cicladoeditado@email.com")]
    public async Task EditarContato_DeveRetornarComSucesso_QuandoParametrosValidos(Guid id, string ddd, string numero,
        string nome, string email)
    {
        // Arrange
        var contatoRequest = new AtualizaContatoRequest
        {
            Nome = nome,
            Email = email,
            Telefone = new TelefoneRequest
            {
                Ddd = ddd,
                Numero = numero
            }
        };

        // Act
        var putResponse = await Client.PutAsJsonAsync($"/api/v1/contatos/{id}", contatoRequest);

        // Assert
        putResponse.EnsureSuccessStatusCode();
        var contato = await DbContext.Contatos.FirstOrDefaultAsync(c => c.Email == email);
        contato.Should().NotBeNull();
        contato.Telefone.Ddd.Should().Be(ddd);
        contato.Telefone.Numero.Should().Be(numero);
        contato.Nome.Should().Be(nome);
    }

    [Theory(DisplayName = "Busca contatos por DDD com sucesso"), TestPriority(3)]
    [InlineData("11")]
    [InlineData("21")]
    public async Task BuscaContato_DeveRetornarComSucesso_QuandoParametrosValidos(string ddd)
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

    [Theory(DisplayName = "Remove contato com sucesso"), TestPriority(4)]
    [InlineData("C1B1DC14-ABAD-483E-87A9-BF46954C8E10")]
    public async Task RemoverContato_DeveRemoverContatoComSucesso_QuandoIdValidoExistente(Guid id)
    {
        // Act
        var response = await Client.DeleteAsync($"/api/v1/contatos/{id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var contato = DbContext.Contatos.FirstOrDefault(c => c.Id == id);
        contato.Should().BeNull();
    }
}