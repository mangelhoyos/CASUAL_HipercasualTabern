using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manejo de estados del menú principal con las funciones ejecutadas por los botones del mismo
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    private const string GAMESCENENAME = "Game";

    [Header("Leaderboard setup")]
    [SerializeField] private ScoreTableHandler scoreTableHandler;
    
    /// <summary>
    /// Inicia la partida enviando al jugador a la escena de juego
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(GAMESCENENAME);
    }

    /// <summary>
    /// Inicializa la tabla de puntaje llamando a la REST API
    /// </summary>
    public void InitializeLeaderboard()
    {
        scoreTableHandler.InitializeScoreTable();
    }

    /// <summary>
    /// Cierra la aplicación
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
