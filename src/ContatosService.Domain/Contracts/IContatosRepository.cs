using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Contracts;

public interface IContatosRepository
{
    Task Create(Contato contato);

    Task Update(Contato contato);

    Task<IEnumerable<Contato>> BuscaRegiao(string? ddd);

    Task<Contato> BuscaId(Guid id);

    Task Remove(Contato contato);

}