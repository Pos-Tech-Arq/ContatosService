using AutoFixture;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.ValueObjects;
using ContatosService.Infra.Services;
using FluentAssertions;
using Moq;

namespace ContatosService.UnitTests.Domain.Entities;

public class ContatoTests
{
    private Mock<IRegiaoRepository> _regiaoRepository = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task AdicionaRegiao_RegiaoAdicionadaDeveSerEquivalente()
    {
        var cidades = new List<Cidade>();
        cidades.Add(new Cidade(_fixture.Create<string>()));
        const string ddd = "88";
        var regiao = new Regiao(ddd, cidades, _fixture.Create<string>());
        _regiaoRepository.Setup(c => c.GetByDdd(ddd)).ReturnsAsync(regiao);
        var contato = new Contato("nomexpto", "emailxpto", new Telefone(ddd, "99999999"));

        await contato.AdicionaRegiao(_regiaoRepository.Object, new Mock<IBuscaRegiaoService>().Object);

        contato.Regiao.Should().BeEquivalentTo(regiao);
    }
}