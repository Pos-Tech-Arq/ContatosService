namespace ContatosService.Domain.Entities;

public class Regiao : Entidade
{
    public IEnumerable<string> DDDs { get; private set; }
    public string Nome { get; private set; }
}