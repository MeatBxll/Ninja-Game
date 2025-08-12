using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
public class MainMenu : MonoBehaviour
{
    public GameObject[] Menus;
    private gameController gameController;
    private InputAction anyButtonAction;
    [SerializeField] private GameObject pressAnyButtonBackground;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        StartListening();
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

    public void LoadMap(int i)
    {
        gameController.currentPlayerCharacterIndex = i;
        gameController.GetComponent<LevelProgress>().saveSlot = i;
        gameController.GetComponent<LevelProgress>().LoadProgress();
        gameController.LoadMap(1);
    }

    public void DeleteSave(int index)
    {
        gameController.GetComponent<LevelProgress>().saveSlot = index;
        gameController.GetComponent<LevelProgress>().ResetProgress();
    }

    public void StartListening()
    {
        anyButtonAction = new InputAction(
            name: "AnyButton",
            type: InputActionType.Button,
            binding: "<Keyboard>/anyKey"
        );

        anyButtonAction.AddBinding("<Mouse>/leftButton");
        anyButtonAction.AddBinding("<Mouse>/rightButton");
        anyButtonAction.AddBinding("<Gamepad>/*");
        anyButtonAction.AddBinding("<Mouse>/leftButton");
        anyButtonAction.AddBinding("<Mouse>/rightButton");
        anyButtonAction.AddBinding("<Mouse>/middleButton");
        anyButtonAction.AddBinding("<Mouse>/backButton");
        anyButtonAction.AddBinding("<Mouse>/forwardButton");
        anyButtonAction.performed += ctx =>
        {
            ChooseMenu(0);
            pressAnyButtonBackground.SetActive(false);
            StopListening();
        };

        anyButtonAction.Enable();
    }

    private void StopListening()
    {
        anyButtonAction.Disable();
        anyButtonAction = null;
    }

}

