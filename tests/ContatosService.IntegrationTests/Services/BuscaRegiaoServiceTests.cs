using System.Net;
using ContatosService.Infra.ExternalServices.BrasilApiService;
using ContatosService.Infra.ExternalServices.BrasilApiService.Responses;
using ContatosService.Infra.Services;
using ContatosService.IntegrationTests.Order;
using Moq;
using Refit;

namespace ContatosService.IntegrationTests.Services;

public class BuscaRegiaoServiceTests(ContatosServiceFactory factory) : Infra.IntegrationTests(factory)
{
    [Theory(DisplayName = "Retorna regiao com sucesso"), TestPriority(4)]
    [InlineData("11")]
    public async Task BuscaRegiao_DeveRetornarComSucesso_QuandoCodigoValido(string ddd)
    {
        // Arrange
        var brasilApi = new Mock<IBrasilApi>();
        brasilApi.Setup(b => b.BuscaRegiaoPorDdd(ddd)).ReturnsAsync(new Region
        {
            State = "SP",
            cities = new List<string> { "São Paulo", "Osasco" }
        });
        var regiaoService = new BuscaRegiaoService(brasilApi.Object);
        
        // Act
        var regiao = await regiaoService.BuscaRegiao(ddd);

        // Assert
        regiao.Should().NotBeNull();
        regiao.Ddd.Should().Be(ddd);
        regiao.Cidades.Should().NotBeEmpty();
    }
    
    [Theory(DisplayName = "Retorna not found e mensagem de erro quando ddd inválido"), TestPriority(4)]
    [InlineData("59")]
    public async Task BuscaRegiao_DeveRetornarNotFound_QuandoCodigoInvalido(string ddd)
    {
        // Arrange
        var brasilApi = new Mock<IBrasilApi>();
        var apiException = ApiException.Create(
            new HttpRequestMessage(HttpMethod.Get, $"https://brasilapi.com.br/api/ddd/v1/{ddd}"),
            HttpMethod.Get,
            new HttpResponseMessage(HttpStatusCode.NotFound),
            new RefitSettings()).Result;
        
        brasilApi.Setup(b => b.BuscaRegiaoPorDdd(ddd)).ReturnsAsync(() => throw apiException);
        var regiaoService = new BuscaRegiaoService(brasilApi.Object);
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => regiaoService.BuscaRegiao(ddd));
        Assert.Equal("Código de região inválido", exception.Message);
    }
}