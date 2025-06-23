using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escMenu : MonoBehaviour
{
    private bool gameIsPaused = false;
    public GameObject pauseMenu;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused == false)
            {
                Pause();
               
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Debug.Log("loading main menu");
    }

    public void QuitGame()
    {
        Debug.Log("quitting game");
        Application.Quit();
    }

}
