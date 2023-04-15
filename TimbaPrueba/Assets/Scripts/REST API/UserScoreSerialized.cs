using Newtonsoft.Json;

/// <summary>
/// Datos serializados que se recogen de la base de datos
/// </summary>
public class UserScoreSerialized
{
    [JsonProperty("usuario", NullValueHandling = NullValueHandling.Ignore)]
    public string usuario { get; set; }
    [JsonProperty("puntaje", NullValueHandling = NullValueHandling.Ignore)]
    public int puntaje { get; set; }
}
