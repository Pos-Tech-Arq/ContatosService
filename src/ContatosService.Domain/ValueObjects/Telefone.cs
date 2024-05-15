namespace ContatosService.Domain.ValueObjects;

public class Telefone
{
    public string Ddd { get; private set; }
    public string Numero { get; private set; }

    public Telefone(string ddd, string numero)
    {
        Ddd = ddd;
        Numero = numero;
    }
}