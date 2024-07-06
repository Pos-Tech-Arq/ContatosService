using ContatosService.Infra.ExternalServices.BrasilApiService.Responses;
using Refit;

namespace ContatosService.Infra.ExternalServices.BrasilApiService;

public interface IBrasilApi
{
    [Get("/api/ddd/v1/{ddd}")]
    Task<Region> BuscaRegiaoPorDdd(string ddd);
}