using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class RebindsScript : MonoBehaviour
{
    [SerializeField] private GameObject WaitingForInputOverlay;
    [SerializeField] private TMP_Text[] BindingTexts;
    [SerializeField] private InputSystemUIInputModule EventSystem;

    private string[] actionNames;
    private int currentKeybindIndex;
    private string oldKeyPath;

    void Start()
    {
        actionNames = UserInput.instance.actionNames;
        UpdateBindingDisplay();
    }

    public void UpdateBindingDisplay()
    {
        for (int i = 0; i < BindingTexts.Length; i++)
        {
            int actionIndex = i % actionNames.Length;
            int bindingIndex = (i < actionNames.Length) ? 0 : 1;

            string display = UserInput.instance.GetBindingDisplayString(actionNames[actionIndex], bindingIndex);
            BindingTexts[i].text = display;
        }
    }

    public void ChangeKeybind(int keybindIndex)
    {
        currentKeybindIndex = keybindIndex;

        int actionIndex = keybindIndex % actionNames.Length;
        int bindingIndex = (keybindIndex < actionNames.Length) ? 0 : 1;

        oldKeyPath = UserInput.instance.GetBindingPath(actionNames[actionIndex], bindingIndex);

        WaitingForInputOverlay.SetActive(true);

        if (EventSystem != null)
            EventSystem.enabled = false;

        bool expectGamepad = bindingIndex == 1;

        UserInput.instance.RebindAction(actionNames[actionIndex], bindingIndex, expectGamepad, OnRebindComplete);
    }

    private void OnRebindComplete()
    {
        if (EventSystem != null)
            EventSystem.enabled = true;

        int actionIndex = currentKeybindIndex % actionNames.Length;
        int bindingIndex = (currentKeybindIndex < actionNames.Length) ? 0 : 1;
        string actionName = actionNames[actionIndex];

        string newKeyPath = UserInput.instance.GetBindingPath(actionName, bindingIndex);

        if (newKeyPath == oldKeyPath)
        {
            WaitingForInputOverlay.SetActive(false);
            return;
        }

        foreach (var otherActionName in actionNames)
        {
            if (otherActionName == actionName)
                continue;

            for (int i = 0; i < 2; i++)
            {
                string otherKeyPath = UserInput.instance.GetBindingPath(otherActionName, i);
                if (otherKeyPath == newKeyPath)
                {
                    UserInput.instance.AssignKeyToAction(otherActionName, i, oldKeyPath);
                }
            }
        }

        UpdateBindingDisplay();
        WaitingForInputOverlay.SetActive(false);
    }

    public void ResetKeybind(int keybindIndex)
    {
        currentKeybindIndex = keybindIndex;

        int actionIndex = keybindIndex % actionNames.Length;
        int bindingIndex = (keybindIndex < actionNames.Length) ? 0 : 1;
        string actionName = actionNames[actionIndex];

        string oldKeyPath = UserInput.instance.GetBindingPath(actionName, bindingIndex);

        WaitingForInputOverlay.SetActive(true);

        UserInput.instance.ResetKeybind(actionName, bindingIndex);

        string newKeyPath = UserInput.instance.GetBindingPath(actionName, bindingIndex);
        foreach (var otherActionName in actionNames)
        {
            if (otherActionName == actionName)
                continue;
            for (int i = 0; i < 2; i++)
            {
                string otherKeyPath = UserInput.instance.GetBindingPath(otherActionName, i);
                if (otherKeyPath == newKeyPath)
                {
                    UserInput.instance.AssignKeyToAction(otherActionName, i, oldKeyPath);
                }
            }
        }

        UpdateBindingDisplay();
        WaitingForInputOverlay.SetActive(false);
    }
}
