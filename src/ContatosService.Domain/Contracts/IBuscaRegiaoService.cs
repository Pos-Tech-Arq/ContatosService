using ContatosService.Domain.Entities;

namespace ContatosService.Infra.Services;

public interface IBuscaRegiaoService
{
    Task<Regiao> BuscaRegiao(string ddd);
}