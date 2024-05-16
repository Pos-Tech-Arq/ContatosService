using AutoFixture;
using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.Services;
using Moq;

namespace ContatosService.Tests;

public class CriaContatoServiceTests
{
    private Mock<IContatosRepository> _contatosRepository = new Mock<IContatosRepository>();
    private Mock<IRegiaoRepository> _regiaoRepository = new Mock<IRegiaoRepository>();
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task Handle_ComDadosValidos_DeveCriarContatoComSucesso()
    {
        var command = _fixture.Create<CriaContatoCommand>();
        var cidades = new List<Cidade>();
        cidades.Add(new Cidade(_fixture.Create<string>()));
        var regiao = new Regiao(command.Ddd, cidades, _fixture.Create<string>());
        _regiaoRepository.Setup(c => c.Get(command.Ddd)).Returns(regiao);
        var service = new CriaContatoService(_contatosRepository.Object, _regiaoRepository.Object);

        await service.Handle(command);

        _contatosRepository.Verify(c => c.Create(It.IsAny<Contato>()), Times.Once);
    }
}