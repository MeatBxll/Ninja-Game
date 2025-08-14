using System;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] menus;
    [SerializeField] private GameObject[] LevelIndicators;
    [SerializeField] private TMP_Text[] prevCurrentAndNextTexts;
    [SerializeField] private GameObject[] allLevelCheckMarks;
    private bool gameIsPaused = false;
    private int currentLevelIndex;

    void Start()
    {
        LevelProgress gameController = GameObject.FindAnyObjectByType<LevelProgress>().GetComponent<LevelProgress>();
        if (gameController.completedLevels.Count != 0)
        {
            foreach (int i in gameController.completedLevels)
            {
                allLevelCheckMarks[i - 2].SetActive(true);
            }
        }
    }
    void Update()
    {
        if (UserInput.instance.pauseInput)
            PauseGame();
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (!gameIsPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }

        OpenMenu(0);
    }

    public void OpenMenu(int _menuIndex)
    {
        foreach (GameObject g in menus)
        {
            g.SetActive(false);
        }
        menus[_menuIndex].SetActive(true);
    }

    public void QuitToMainMenu()
    {
        PauseGame();
        SceneManager.LoadScene(0);
    }

    public void SwapLevelIndicator(bool isNext)
    {
        if (isNext)
            currentLevelIndex = currentLevelIndex == 4 ? 0 : currentLevelIndex + 1;
        else
            currentLevelIndex = currentLevelIndex == 0 ? 4 : currentLevelIndex - 1;

        int next = currentLevelIndex == 4 ? 0 : currentLevelIndex + 1;
        int prev = currentLevelIndex == 0 ? 4 : currentLevelIndex - 1;

        prevCurrentAndNextTexts[0].text = LevelIndicators[prev].name;
        prevCurrentAndNextTexts[1].text = LevelIndicators[currentLevelIndex].name;
        prevCurrentAndNextTexts[2].text = LevelIndicators[next].name;

        LevelIndicators[currentLevelIndex].SetActive(true);
        LevelIndicators[prev].SetActive(false);
        LevelIndicators[next].SetActive(false);
    }
}
