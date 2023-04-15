using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Maneja el envio y obtenci�n de datos
/// </summary>
public class APIRequestHandler : MonoBehaviour
{

    /// <summary>
    /// UnityWebRequest a una REST API para obtener o enviar datos
    /// </summary>
    /// <param name="requestData">Los parametros para realizar el request</param>
    public async Task<string> MakeHTTPRequest(RequestDataSO requestData)
    {
        string response = string.Empty;
        switch (requestData.GetHTTPVerb())
        {
            case RequestDataSO.HTTPVerbs.GET:
                using (UnityWebRequest request = UnityWebRequest.Get(requestData.GenerateURI()))
                {
                    UnityWebRequestAsyncOperation operation = request.SendWebRequest();

                    while (!operation.isDone)
                        await Task.Yield();

                    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log("Request error :: " + request.error);
                    }
                    else
                    {
                        response = request.downloadHandler.text;
                        Debug.Log("Request successful :: " + request.downloadHandler.text);
                    }
                }
                break;

            case RequestDataSO.HTTPVerbs.POST:
                UserScoreSerialized user = new UserScoreSerialized();
                user.usuario = "Raul";
                user.puntaje = 780;
                string jsonFileToSend = JsonConvert.SerializeObject(user);
                using(UnityWebRequest request = UnityWebRequest.Put(requestData.GenerateURI(),jsonFileToSend))
                {
                    request.method = "POST";
                    request.SetRequestHeader("Content-Type", "application/json");

                    UnityWebRequestAsyncOperation operation = request.SendWebRequest();

                    while (!operation.isDone)
                        await Task.Yield();

                    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log("Request error :: " + request.error);
                    }
                    else
                    {
                        Debug.Log("POST Request succesful :: " + jsonFileToSend);
                    }
                }
                break;

            default:
                Debug.LogWarning("VERB NOT SELECTED FOR :: " + requestData.name);
                break;

        }
        return response;
    }
}