using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Contracts;

public interface IContatosRepository
{
    Task CreateContatos(Contato contato);
}