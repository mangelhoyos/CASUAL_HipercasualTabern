using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Genera la tabla de puntuación en base a los resultados de la base de datos
/// </summary>
public class ScoreTableHandler : MonoBehaviour
{
    [Header("API Setup")]
    [SerializeField]
    private APIRequestHandler APIHandler;
    [SerializeField]
    private RequestDataSO getScoresRequest;

    [Header("UI Setup")]
    [SerializeField]
    private GameObject userElementReference; //Element for factory pattern
    [SerializeField]
    private GameObject loadingPanel;

    /// <summary>
    /// Inicializa la tabla solicitando los datos de puntuación
    /// </summary>
    public async void InitializeScoreTable()
    {
        loadingPanel.SetActive(true);

        string response = await APIHandler.MakeHTTPRequest(getScoresRequest);
        List<UserScoreSerialized> result = JsonConvert.DeserializeObject<List<UserScoreSerialized>>(response);

        GenerateTable(result);
    }

    private void GenerateTable(List<UserScoreSerialized> scores)
    {

    }
}
