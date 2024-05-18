using AutoFixture;
using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.Services;
using FluentAssertions;
using Moq;

namespace ContatosService.UnitTests.Domain.Services;

public class CriaContatoServiceTests
{
    private Mock<IContatosRepository> _contatosRepository = new();
    private Mock<IRegiaoRepository> _regiaoRepository = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Handle_ComDadosValidos_DeveCriarContatoComSucesso()
    {
        var command = _fixture.Create<CriaContatoCommand>();
        var cidades = new List<Cidade>();
        var cidade = new Cidade(_fixture.Create<string>());
        cidades.Add(cidade);
        var regiao = new Regiao(command.Ddd, cidades, _fixture.Create<string>());
        _regiaoRepository.Setup(c => c.Get(command.Ddd)).Returns(regiao);
        var service = new CriaContatoService(_contatosRepository.Object, _regiaoRepository.Object);

        await service.Handle(command);

        _contatosRepository.Verify(c => c.Create(It.Is<Contato>(
             contato => contato.Regiao.Ddd == regiao.Ddd &&
                        contato.Regiao.Estado == regiao.Estado &&
                        contato.Regiao.Cidades.Any(x => x.Nome == cidade.Nome) &&
                        contato.Nome == command.Nome &&
                        contato.Email == command.Email &&
                        contato.Telefone.Ddd == command.Ddd &&
                        contato.Telefone.Numero == command.Numero
                        )), Times.Once);
    }
}