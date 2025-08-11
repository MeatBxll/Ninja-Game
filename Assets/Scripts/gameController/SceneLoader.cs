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
    [SerializeField] private GameObject startRoomDoor;
    private int currentLevelIndex;
    void Start()
    {
        foreach (LevelPlate g in levelPlates)
        {
            g.SceneLoader = gameObject.GetComponent<SceneLoader>();
        }

        gameController = GameObject.FindWithTag("GameController").GetComponent<gameController>();
        gameController.SceneLoaded(true);

        // make all the initial map details based on game progress here
        foreach (int i in gameController.completedLevelIndexes)
        {
            if (i == 2)
            {
                startRoomDoor.SetActive(false);
            }
        }
    }

    public void HoveringLevel(GameObject levelPlate)
    {
        LevelTitle.text = levelPlate.name;
        currentLevelIndex = levelPlate.GetComponent<LevelPlate>().sceneIndex;
        gameController.playerSpawn = new Vector3(levelPlate.transform.position.x, levelPlate.transform.position.y, 1);
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
        gameController.currentLevelIndex = currentLevelIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
}
