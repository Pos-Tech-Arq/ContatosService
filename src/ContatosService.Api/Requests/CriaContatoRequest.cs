namespace ContatosService.Api.Requests;

public class CriaContatoRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public TelefoneRequest Telefone { get; set; } = new();
}