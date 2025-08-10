using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public GameObject SceneUi;
    [SerializeField] private TMP_Text LevelTitle;
    [SerializeField] private LevelPlate[] levelPlates;
    private gameController gameController;

    private int currentLevelIndex;
    void Start()
    {
        foreach (LevelPlate g in levelPlates)
        {
            g.SceneLoader = gameObject.GetComponent<SceneLoader>();
        }

        gameController = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        gameController.SceneLoaded(true);
    }

    public void HoveringLevel(GameObject levelPlate)
    {
        LevelTitle.text = levelPlate.name;
        currentLevelIndex = levelPlate.GetComponent<LevelPlate>().sceneIndex;
        SceneUi.SetActive(true);
    }

    public void StopHoveringLevel()
    {
        SceneUi.SetActive(false);
    }

    public void enterLevel()
    {
        Collider2D[] colliders = gameController.currentPlayerInstance.GetComponents<Collider2D>();

        foreach (Collider2D col in colliders)
        {
            Destroy(col);
        }
        SceneManager.LoadScene(currentLevelIndex);
    }
}
