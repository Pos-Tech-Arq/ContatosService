using ContatosService.Domain.Contracts;
using ContatosService.Domain.Entities;
using ContatosService.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

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

    public IEnumerable<Contato>GetAll()
    {
         return _applicationDbContext.Set<Contato>().ToList();
    }
}