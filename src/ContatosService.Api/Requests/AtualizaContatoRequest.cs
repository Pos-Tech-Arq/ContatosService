namespace ContatosService.Api.Requests;

public class AtualizaContatoRequest: CriaContatoRequest
{
    public Guid Id { get; set; }
}