using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Maneja el estado general del juego
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private GameObject usernameInputPanel;
    [SerializeField] private TMP_Text scoreTextField;
    [SerializeField] private TMP_Text finalScoreTextField;

    [Header("Request")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject datosPanel;
    [SerializeField] private GameObject reintentarEnviarDatosButton;
    [SerializeField] private RequestDataSO requestData;
    [SerializeField] private TMP_Text errorTextField;

    [Header("Callbacks")]
    [SerializeField] private UnityEvent OnGameOver;

    private int actualScore;

    private UserScoreSerialized actualUser = new UserScoreSerialized();

    private void Start()
    {
        Time.timeScale = 0; //Juego no empieza hasta que se reciba el username
    }

    /// <summary>
    /// Suma puntuación
    /// </summary>
    /// <param name="scoreToAdd">La puntuación que se sumará a la actual</param>
    public void AddScore(int scoreToAdd)
    {
        AudioManager.instance.Play("Point");
        actualScore += scoreToAdd;
        scoreTextField.text = actualScore.ToString();
    }

    /// <summary>
    /// Termina el juego como GameOver
    /// </summary>
    public void GameOver()
    {
        AudioManager.instance.Play("End");
        Time.timeScale = 0;
        OnGameOver?.Invoke();

        SendData();
    }

    /// <summary>
    /// Envia los datos a la base de datos
    /// </summary>
    public async void SendData()
    {
        errorTextField.text = "SUBIENDO LOS DATOS...";
        reintentarEnviarDatosButton.SetActive(false);

        finalScoreTextField.text = actualScore.ToString();
        actualUser.puntaje = actualScore;
        requestData.userScoreToSend = actualUser;

        gameOverPanel.SetActive(true);

        string response = await APIRequestHandler.Instance.MakeHTTPRequest(requestData);

        if (response != "ERROR")
        {
            datosPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Data coulnd't been sent");
            errorTextField.text = "ERROR ENVIANDO LOS DATOS";
            reintentarEnviarDatosButton.SetActive(true);
        }

        requestData.userScoreToSend = null;
    }

    /// <summary>
    /// Establece el nombre de usuario de la persona y empieza el juego
    /// </summary>
    public void StartGame()
    {
        if (string.IsNullOrEmpty(usernameInput.text))
            return;

        usernameInputPanel.SetActive(false);
        actualUser.usuario = usernameInput.text;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Devuelve al jugador al menú principal
    /// </summary>
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Reinicia el juego volviendo a cargar la escena actual
    /// </summary>
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
