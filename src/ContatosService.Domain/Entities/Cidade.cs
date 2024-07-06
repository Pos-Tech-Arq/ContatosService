namespace ContatosService.Domain.Entities;

public class Cidade : Entidade
{
    public string Nome { get; private set; }
    public Regiao Regiao { get; set; }

    public Cidade(string nome)
    {
        Id = Guid.NewGuid();
        Nome = nome;
    }

    private Cidade()
    {
    }
}