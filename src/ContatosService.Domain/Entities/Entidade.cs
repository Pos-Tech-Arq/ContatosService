namespace ContatosService.Domain.Entities;

public abstract class Entidade
{
    public Guid Id { get; protected set; }

    protected Entidade()
    {
    }
}