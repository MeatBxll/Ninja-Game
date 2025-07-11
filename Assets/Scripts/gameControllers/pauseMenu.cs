using System;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    private bool gameIsPaused = false;
    [SerializeField] private GameObject pausePanel;
    [NonSerialized] public KeyCode pauseButton = KeyCode.Escape;
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
            PauseGame();
    }

    void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (!gameIsPaused)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;

        pausePanel.SetActive(gameIsPaused);
    }
}
