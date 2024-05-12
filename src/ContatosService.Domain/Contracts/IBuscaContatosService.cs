using ContatosService.Domain.Commands;
using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Contracts;

public interface IBuscaContatosService
{
    IEnumerable<Contato> Handle();
}