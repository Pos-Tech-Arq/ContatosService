using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.ValueObjects;

namespace ContatosService.Domain.Services;

public class CriaContatoService : ICriaContatoService
{
    private readonly IContatosRepository _contatosRepository;

    public CriaContatoService(IContatosRepository contatosRepository)
    {
        _contatosRepository = contatosRepository;
    }

    public Task Handle(CriaContatoCommand command)
    {
        var telefone = new Telefone(command.Ddd, command.Numero);
        var contato = new Contato(command.Nome, command.Email, telefone);
        contato.AdicionaRegiao();

        return _contatosRepository.Create(contato);
    }
}