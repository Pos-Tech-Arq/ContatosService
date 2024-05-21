using ContatosService.Domain.Contracts;
using ContatosService.Domain.ValueObjects;
using ContatosService.Infra.Services;

namespace ContatosService.Domain.Entities;

public class Contato : Entidade, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public Regiao Regiao { get; private set; }

    public Contato(string nome, string email,Telefone telefone)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Id = Guid.NewGuid();
    }

    public Contato(Guid id,string nome, string email, Telefone telefone)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Id = id;
    }

    //TODO Deve receber como parametro o serviço e as informações nescessário para adicionar a região
    public async Task AdicionaRegiao(IRegiaoRepository regiaoRepository, IBuscaRegiaoService buscaRegiaoService)
    {
        Regiao = await regiaoRepository.GetByDdd(Telefone.Ddd) ?? await buscaRegiaoService.BuscaRegiao(Telefone.Ddd);
    }

    private Contato()
    {
    }
}