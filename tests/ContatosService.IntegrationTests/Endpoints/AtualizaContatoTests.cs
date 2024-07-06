using System.Net.Http.Json;
using ContatosService.Api.Requests;
using Microsoft.EntityFrameworkCore;

namespace ContatosService.IntegrationTests.Endpoints;

public class AtualizaContatoTests(ContatosServiceFactory factory) : Infra.IntegrationTests(factory)
{
    [Theory(DisplayName = "Edita contato com sucesso")]
    [InlineData("6238B691-480F-47B3-B2CC-29BCCD5B3BD2", "11", "999999999", "Fulano Editado", "fulanoeditado@email.com")]
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
}