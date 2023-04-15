using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Genera la tabla de puntuación en base a los resultados de la base de datos
/// </summary>
public class ScoreTableHandler : MonoBehaviour
{
    [Header("API Setup")]
    [SerializeField] private RequestDataSO getScoresRequest;
    private APIRequestHandler APIHandler;

    [Header("UI Setup")]
    [SerializeField] private TableElement userElementReference; //Element for factory pattern
    [SerializeField] private Transform scrollViewContentTransform;
    [SerializeField] private GameObject loadingPanel;

    private List<GameObject> generatedScores = new List<GameObject>(); //Lista de elementos actuales generados en la tabla

    private void Start() => APIHandler = APIRequestHandler.Instance;

    /// <summary>
    /// Inicializa la tabla solicitando los datos de puntuación
    /// </summary>
    public async void InitializeScoreTable()
    {
        loadingPanel.SetActive(true);

        string response = await APIHandler.MakeHTTPRequest(getScoresRequest);
        if (!string.IsNullOrEmpty(response))
        {
            List<UserScoreSerialized> result = JsonConvert.DeserializeObject<List<UserScoreSerialized>>(response);
            GenerateTable(result);
        }
        else
        {
            Debug.LogWarning("Table couldn't be created, didn't get a response from Database");
            loadingPanel.SetActive(false);
        }
    }

    private void GenerateTable(List<UserScoreSerialized> scores)
    {
        //Limpiar la tabla
        if(generatedScores.Count > 0)
        {
            foreach(GameObject listElement in generatedScores)
            {
                Destroy(listElement);
            }
            generatedScores = new List<GameObject>();
        }

        //Generar la tabla con los usuarios con factory pattern
        try
        {
            int scoreIndex = 1;
            foreach (UserScoreSerialized score in scores)
            {
                if(!string.IsNullOrEmpty(score.usuario))
                {
                    TableElement newElement = Instantiate(userElementReference.gameObject, scrollViewContentTransform).GetComponent<TableElement>();
                    newElement.SetTableElementData(scoreIndex.ToString(), score.usuario, score.puntaje.ToString());
                    generatedScores.Add(newElement.gameObject);
                    newElement.gameObject.SetActive(true);
                    scoreIndex++;
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error creating table :: " + e.Message);
        }

        loadingPanel.SetActive(false);
    }
}
