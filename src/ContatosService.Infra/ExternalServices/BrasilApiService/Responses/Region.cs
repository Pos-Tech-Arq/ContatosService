using System.Text.Json.Serialization;

namespace ContatosService.Infra.ExternalServices.BrasilApiService.Responses;

public class Region
{
    
    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("cities")]
    public List<string> cities { get; set; }
}