using AutoFixture;
using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.Services;
using Moq;

namespace ContatosService.Tests;

public class CriaContatoServiceTests
{
    [Fact]
    public async Task Handle_ComDadosValidos_DeveCriarContatoComSucesso()
    {
        var contatosRepository = new Mock<IContatosRepository>();
        var regiaoRepository = new Mock<IRegiaoRepository>();
        var fixture = new Fixture();
        var command = fixture.Create<CriaContatoCommand>();
        var cidades = new List<Cidade>();
        cidades.Add(new Cidade(fixture.Create<string>()));
        var regiao = new Regiao(command.Ddd, cidades, fixture.Create<string>());
        regiaoRepository.Setup(c => c.Get(command.Ddd)).Returns(regiao);
        var service = new CriaContatoService(contatosRepository.Object, regiaoRepository.Object);

        await service.Handle(command);

        contatosRepository.Verify(c => c.Create(It.IsAny<Contato>()), Times.Once);
    }
}