using ContatosService.Domain.Contracts;
using ContatosService.Domain.ValueObjects;

namespace ContatosService.Domain.Entities;

public class Contato : Entidade, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public Regiao Regiao { get; private set; }

    public Contato(string nome, string email,Telefone telefone)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nome, "O Nome nao eh valido.");
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Id = Guid.NewGuid();
    }

    //TODO Deve receber como parametro o serviço e as informações nescessário para adicionar a região
    public void AdicionaRegiao()
    {
        //TODO Deve criar a região e atribuir.
    }

    private Contato()
    {
    }
}