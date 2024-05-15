using ContatosService.Domain.Entities;

namespace ContatosService.Domain.Contracts;

public interface IRegiaoRepository
{
    Task Create(Regiao contato);

    Regiao Get(string ddd);
}