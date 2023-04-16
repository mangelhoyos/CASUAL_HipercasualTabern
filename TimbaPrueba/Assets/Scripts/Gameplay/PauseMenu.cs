using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
