namespace ContatosService.IntegrationTests.Endpoints;

public class RemoveContatosTests(ContatosServiceFactory factory) : Infra.IntegrationTests(factory)
{
    [Theory(DisplayName = "Remove contato com sucesso")]
    [InlineData("6238B691-480F-47B3-B2CC-29BCCD5B3BD2")]
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