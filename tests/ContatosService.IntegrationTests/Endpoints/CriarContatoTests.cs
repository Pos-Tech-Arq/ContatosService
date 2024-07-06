using System.Net.Http.Json;
using ContatosService.Api.Requests;
using Microsoft.EntityFrameworkCore;

namespace ContatosService.IntegrationTests.Endpoints;

public class CriarContatoTests(ContatosServiceFactory factory) : Infra.IntegrationTests(factory)
{
    [Theory(DisplayName = "Cria novo contato com sucesso")]
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
}