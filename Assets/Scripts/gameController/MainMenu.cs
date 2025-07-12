using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Menus;
    private gameController gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
    }

    public void ChooseMenu(int m)
    {
        foreach (GameObject i in Menus)
            i.SetActive(false);
        Menus[m].SetActive(true);
    }

    public void ChooseYourCharacter(int i)
    {
        gameController.currentPlayerCharacterIndex = i;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
