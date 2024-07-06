namespace ContatosService.IntegrationTests.ApiResponses;

public class Contato
{
    public string nome { get; set; }
    public string email { get; set; }
    public Telefone telefone { get; set; }
    public Regiao regiao { get; set; }
    public string id { get; set; }
}