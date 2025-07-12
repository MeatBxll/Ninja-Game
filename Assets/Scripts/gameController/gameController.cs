using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    [SerializeField] private GameObject[] allPlayerCharacters;
    public int currentPlayerCharacterIndex;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
    }
}
