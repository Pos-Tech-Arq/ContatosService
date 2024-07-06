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

    public async Task<Regiao?> GetByDdd(string ddd)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Ddd == ddd);
    }
}