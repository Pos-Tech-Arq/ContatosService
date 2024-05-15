using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Net.Http.Json;

namespace ContatosService.Infra.Repositories;

public class RegiaoRepository : IRegiaoRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private DbSet<Regiao> _dbSet;

    public RegiaoRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Regioes;
    }

    public async Task Create(Regiao regiao)
    {
        await _dbSet.AddAsync(regiao);
        await _applicationDbContext.SaveChangesAsync();
    }

    public Regiao Get(string ddd)
    {
        
        var Regiao = _applicationDbContext.Set<Regiao>().Where(d=>d.Ddd ==ddd).FirstOrDefault();

        return Regiao ?? BuscaRegiao(ddd).Result;
    }

    public async Task<Regiao> BuscaRegiao(string ddd)
    {
        HttpClient client = new HttpClient();
        Regiao regiao = null;
        HttpResponseMessage response = await  client.GetAsync($"https://brasilapi.com.br/api/ddd/v1/{ddd}");
        if (response.IsSuccessStatusCode)
        {
           var region = await response.Content.ReadFromJsonAsync<Region>();
            regiao = new (ddd,region.cities.Select(r=>new Cidade(r)).ToList(), region.State);
        }
        return regiao;
    }

    public class Region
    {
        public string State { get; set; }

        public List<string> cities { get; set; }
    }
}