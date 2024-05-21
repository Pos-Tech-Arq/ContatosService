namespace ContatosService.Domain.Commands;

public class RemoveContatoCommand
{
    public Guid Id { get; set; }

    public RemoveContatoCommand(Guid id)
    {
        Id = id;
    }
}