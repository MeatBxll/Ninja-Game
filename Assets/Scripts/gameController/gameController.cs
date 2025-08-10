using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    [SerializeField] private GameObject[] allPlayerCharacters;
    [SerializeField] private GameObject[] allMapPlayerCharacters;
    [NonSerialized] public int currentPlayerCharacterIndex;
    [NonSerialized] public GameObject currentPlayerInstance;
    [NonSerialized] public Vector3 playerSpawn = new Vector3(-110, 22, 1);
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
            currentPlayerInstance = Instantiate(allMapPlayerCharacters[currentPlayerCharacterIndex], playerSpawn, Quaternion.identity);
            currentPlayerInstance.GetComponent<MapPlayer>().gameController = gameObject.GetComponent<gameController>();
        }
        else
        {
            currentPlayerInstance = Instantiate(allPlayerCharacters[currentPlayerCharacterIndex], playerSpawn, Quaternion.identity);
        }
    }
}
