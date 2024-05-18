using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Contracts;

public interface IRegiaoRepository
{
    Task Create(Regiao contato);

    Task<Regiao> GetByDdd(string ddd);
}