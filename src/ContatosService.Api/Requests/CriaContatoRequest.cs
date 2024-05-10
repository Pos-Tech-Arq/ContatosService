namespace ContatosService.Api.Requests;

public class CriaContatoRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public TelefoneRequest Telefone { get; set; }
}