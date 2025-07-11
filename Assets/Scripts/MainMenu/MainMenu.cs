using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Menus;

    public void ChooseMenu(int m)
    {
        foreach (GameObject i in Menus)
            i.SetActive(false);
        Menus[m].SetActive(true);
    }

    public void ChooseYourCharacter(int i)
    {
        //make an array with all the characters in it then when the index comes in match the character to the index and pass that character into the scene somehow
    }
    public void QuitGame()
    {

    }
}
