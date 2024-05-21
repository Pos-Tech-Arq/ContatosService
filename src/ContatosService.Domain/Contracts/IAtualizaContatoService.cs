using ContatosService.Domain.Commands;

namespace ContatosService.Domain.Contracts;

public interface IAtualizaContatoService
{
    Task Handle(AtualizaContatoCommand command);
}