using System.Text;
using UnityEngine;

/// <summary>
/// Maneja los datos necesarios para realizar el request.
/// Tales como dirección, endpoint, puerto y protocolo HTTP
/// </summary>
[CreateAssetMenu(fileName = "HTTP Request Data", menuName = "REST API/Request Data")]
public class RequestDataSO : ScriptableObject
{
    public enum HTTPVerbs
    {
        NONE,
        GET,
        POST
    }

    [Header("HTTP data")]
    [SerializeField] private string networkLocation;
    [SerializeField] private string networkEndpoint;
    [SerializeField] private HTTPVerbs HTTPVerb;
    [SerializeField] private int listeningPort;

    [HideInInspector] public UserScoreSerialized userScoreToSend;
 
    /// <summary>
    /// Genera el URI basado en los datos del request
    /// </summary>
    /// <returns>URI para realizar el HTTP Request</returns>
    public string GenerateURI()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("{0}:{1}/{2}", networkLocation, listeningPort.ToString(), networkEndpoint);

        return sb.ToString();
    }

    /// <returns>El HTTP Verb utilizado para el request (GET o POST)</returns>
    public HTTPVerbs GetHTTPVerb() => HTTPVerb;
}
