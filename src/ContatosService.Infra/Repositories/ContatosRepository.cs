using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace ContatosService.Infra.Repositories;

public class ContatosRepository : IContatosRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    private DbSet<Contato> _dbSet;

    public ContatosRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Contatos;
    }

    public async Task Create(Contato contato)
    {
        await _dbSet.AddAsync(contato);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task Update(Contato contato)
    {
        _dbSet.Update(contato);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Contato>> BuscaRegiao(string? ddd)
    {
        var query = _dbSet
            .Include(c => c.Regiao)
            .AsQueryable();

        if (!ddd.IsNullOrEmpty())
        {
            query = query
                .Where(c => c.Telefone.Ddd == ddd);
        }

        return await query.ToListAsync();
    }

    public async Task<Contato> BuscaId(Guid id)
    {
        var query = _dbSet
            .Include(c => c.Regiao)
            .AsQueryable().Where(c => c.Id == id);

        return await query.FirstAsync();
    }

    public async Task Remove(Contato contato)
    {
        _dbSet.Remove(contato);
        await _applicationDbContext.SaveChangesAsync();
    }
}