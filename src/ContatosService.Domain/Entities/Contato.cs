using ContatosService.Domain.Contracts;
using ContatosService.Domain.ValueObjects;

namespace ContatosService.Domain.Entities;

public class Contato : Entidade, IAggregateRoot
{
    public string Nome { get; private set; }
    public Telefone Telefone { get; private set; }
    public Regiao Regiao { get; private set; }

    public Contato(string nome, Telefone telefone)
    {
        Id = Guid.NewGuid();
        ArgumentException.ThrowIfNullOrWhiteSpace(nome, "O Nome nao eh valido.");
        Nome = nome;
        Telefone = telefone;
    }

    // TODO Servico que busca regiao por DDD
    public void RegistraRegiao()
    {
        var regiao = new Regiao(); 
    }
}