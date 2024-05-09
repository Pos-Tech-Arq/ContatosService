using ContatosService.Domain.Contracts;
using ContatosService.Domain.ValueObjects;

namespace ContatosService.Domain.Entities;

public class Contato : Entidade, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public Regiao Regiao { get; private set; }

    public Contato(string nome, Telefone telefone, Regiao regiao)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nome, "O Nome nao eh valido.");
        Nome = nome;
        Telefone = telefone;
        Regiao = regiao;
        Id = Guid.NewGuid();
    }

    private Contato()
    {
    }
}