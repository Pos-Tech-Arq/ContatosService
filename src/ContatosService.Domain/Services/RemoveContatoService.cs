using ContatosService.Domain.Commands;
using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Domain.ValueObjects;
using ContatosService.Infra.Services;

namespace ContatosService.Domain.Services;

public class RemoveContatoService : IRemoveContatoService
{
    private readonly IContatosRepository _contatosRepository;

    public RemoveContatoService(IContatosRepository contatosRepository)
    {
        _contatosRepository = contatosRepository;
    }

    public async Task Handle(RemoveContatoCommand command)
    {
        var contato = await _contatosRepository.BuscaId(command.Id);

        await _contatosRepository.Remove(contato);
    }
}