using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    [SerializeField] private GameObject[] allPlayerCharacters;
    [SerializeField] private GameObject[] allMapPlayerCharacters;
    [NonSerialized] public int currentPlayerCharacterIndex;
    [NonSerialized] public GameObject currentPlayerInstance;
    [NonSerialized] public Vector3 playerSpawn;
    [NonSerialized] public Vector3 mapSpawn = new Vector3(-110, 22, 1);
    [NonSerialized] public int sceneIndex;
    [NonSerialized] public int currentLevelIndex;
    [NonSerialized] public static gameController Instance;

    [NonSerialized] public bool firstLoad = true;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void LoadMap(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void SceneLoaded(bool IsMap)
    {
        if (IsMap)
        {
            currentPlayerInstance = Instantiate(allMapPlayerCharacters[currentPlayerCharacterIndex], mapSpawn, Quaternion.identity);
            currentPlayerInstance.GetComponent<MapPlayer>().gameController = gameObject.GetComponent<gameController>();
        }
        else
        {
            currentPlayerInstance = Instantiate(allPlayerCharacters[currentPlayerCharacterIndex], playerSpawn, Quaternion.identity);
        }
    }

    public void LevelCompleted()
    {
        gameObject.GetComponent<LevelProgress>().MarkLevelComplete(currentLevelIndex);
        LoadMap(1);
    }
}
