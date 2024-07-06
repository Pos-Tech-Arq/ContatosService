using ContatosService.Domain.Entities;
using ContatosService.Infra.Contexts.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ContatosService.Infra.Contexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Regiao> Cidades { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ContatoMapping().Configure(modelBuilder.Entity<Contato>());
        new RegiaoMapping().Configure(modelBuilder.Entity<Regiao>());
        new CidadeMapping().Configure(modelBuilder.Entity<Cidade>());
    }
}