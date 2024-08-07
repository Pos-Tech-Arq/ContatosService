﻿using ContatosService.Domain.Entities;
using ContatosService.Infra.ExternalServices;
using ContatosService.Infra.ExternalServices.BrasilApiService;
using Refit;

namespace ContatosService.Infra.Services;

public class BuscaRegiaoService : IBuscaRegiaoService
{
    private readonly IBrasilApi _brasilApi;

    public BuscaRegiaoService(IBrasilApi brasilApi)
    {
        _brasilApi = brasilApi;
    }

    public async Task<Regiao> BuscaRegiao(string ddd)
    {
        try
        {
            var region = await _brasilApi.BuscaRegiaoPorDdd(ddd);

            var cidades = region.cities.Select(c => new Cidade(c)).ToList();

            return new Regiao(ddd, cidades, region.State);
        }
        catch (ApiException e)
        {
            throw new HttpRequestException("Código de região inválido");
        }
    }
}