using ContatosService.Domain.Commands;

namespace ContatosService.Domain.Contracts;

public interface IRemoveContatoService
{
    Task Handle(RemoveContatoCommand command);
}