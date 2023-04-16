using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que maneja el sistema de pausa en forma de toggle
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameConfigurationSO gameConfig;

    private bool isPaused;

    private void Update()
    {
        if(Input.GetKeyDown(gameConfig.pauseButton))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Al llamar esta función activa el pausa si estaba desactivado y si estaba activado lo desactiva
    /// </summary>
    public void TogglePause()
    {
        AudioManager.instance.Play("ButtonClick");

        if(!isPaused)
        {
            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenuPanel.SetActive(false);
            isPaused = false;
        }
    }
}
