using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Contracts;

public interface IContatosRepository
{
    Task Create(Contato contato);
}