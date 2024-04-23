namespace ContatosService.Domain.ValueObjects;

public class Telefone
{
    public string DDD { get; private set; }
    public string Numero { get; private set; }

    public Telefone(string ddd, string numero)
    {
        if (!DddEhValido(ddd))
        {
            throw new ArgumentException("O ddd nao eh valido.");
        }

        DDD = ddd;
        Numero = numero;
    }

    private static bool DddEhValido(string ddd)
    {
        return !string.IsNullOrEmpty(ddd) && ddd.Length == 2;
    }
}