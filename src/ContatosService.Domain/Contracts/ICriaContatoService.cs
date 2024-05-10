using ContatosService.Domain.Commands;

namespace ContatosService.Domain.Contracts;

public interface ICriaContatoService
{
    Task Handle(CriaContatoCommand command);
}