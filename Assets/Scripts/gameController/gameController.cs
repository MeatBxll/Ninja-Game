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
    [NonSerialized] public List<int> completedLevelIndexes;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        completedLevelIndexes = new List<int>();
    }

    public void LoadMap(int i)
    {
        currentPlayerCharacterIndex = i;
        SceneManager.LoadScene(1);
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
        completedLevelIndexes.Add(currentLevelIndex);
        LoadMap(currentPlayerCharacterIndex);
    }
}
