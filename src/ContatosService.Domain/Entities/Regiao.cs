namespace ContatosService.Domain.Entities;

public class Regiao : Entidade
{
    public string Ddd { get; private set; }
    public IEnumerable<Cidade> Cidades { get; private set; }
    public string Estado { get; private set; }

    public Regiao(string ddd, IEnumerable<Cidade> cidades, string estado)
    {
        Ddd = ddd;
        Cidades = cidades;
        Estado = estado;
        Id = Guid.NewGuid();
    }

    private Regiao()
    {
    }
}