using Newtonsoft.Json;

public class UserScoreSerialized
{
    [JsonProperty("usuario")]
    public string usuario { get; set; }
    [JsonProperty("puntaje")]
    public int puntaje { get; set; }
}
