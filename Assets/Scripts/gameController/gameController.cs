using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    [SerializeField] private GameObject[] allPlayerCharacters;
    [NonSerialized] public int currentPlayerCharacterIndex;
    private GameObject currentPlayerInstance;
    private Transform playerSpawn;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadMap(int i)
    {
        currentPlayerCharacterIndex = i;
        SceneManager.LoadScene(1);
    }

    public void SceneLoaded(Transform spawn)
    {
        playerSpawn = spawn;
        currentPlayerInstance = Instantiate(allPlayerCharacters[currentPlayerCharacterIndex], spawn);
    }
}
