using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

    private PlayerInput playerInput;

    public Vector2 moveInput
    {
        get; private set;
    }
    public bool jumpInput
    {
        get; private set;
    }
    public bool attackInput
    {
        get; private set;
    }
    public bool dashInput
    {
        get; private set;
    }
    public bool pauseInput
    {
        get; private set;
    }
    public bool abilityOneInput
    {
        get; private set;
    }

    private InputAction _moveAction;
    private InputAction[] actionList;
    public string[] actionNames = { "Jump", "Dash", "Attack", "AbilityOne", "Pause" };

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("UserInput requires a PlayerInput component!");
            return;
        }

        if (playerInput.devices.Count == 0)
        {
            if (Keyboard.current != null && Mouse.current != null)
                playerInput.SwitchCurrentControlScheme("Keyboard&Mouse", Keyboard.current, Mouse.current);
            else if (Gamepad.current != null)
                playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
        }

        LoadAllBindings();
        SetupInputActions();
    }

    private void SetupInputActions()
    {
        _moveAction = playerInput.actions["Movement"];

        actionList = new InputAction[actionNames.Length];
        for (int i = 0; i < actionNames.Length; i++)
        {
            InputAction act = playerInput.actions[actionNames[i]];
            if (act != null)
                actionList[i] = act;
            else
                Debug.LogWarning($"Action '{actionNames[i]}' not found in PlayerInput.");
        }
    }

    private void Update()
    {
        if (_moveAction != null)
            moveInput = _moveAction.ReadValue<Vector2>();

        for (int i = 0; i < actionNames.Length; i++)
        {
            if (actionList[i] == null)
                continue;
            switch (actionNames[i])
            {
                case "Jump":
                    jumpInput = actionList[i].WasPressedThisFrame();
                    break;
                case "Dash":
                    dashInput = actionList[i].WasPressedThisFrame();
                    break;
                case "Attack":
                    attackInput = actionList[i].WasPressedThisFrame();
                    break;
                case "AbilityOne":
                    abilityOneInput = actionList[i].WasPressedThisFrame();
                    break;
                case "Pause":
                    pauseInput = actionList[i].WasPressedThisFrame();
                    break;
            }
        }
    }

    public void RebindAction(string actionName, int bindingIndex, bool expectGamepad, Action onComplete = null)
    {
        InputAction actionToRebind = playerInput.actions[actionName];
        if (actionToRebind == null)
        {
            Debug.LogWarning($"Action '{actionName}' not found!");
            onComplete?.Invoke();
            return;
        }

        actionToRebind.Disable();

        actionToRebind.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(op =>
            {
                var lastControl = op.selectedControl;
                bool isGamepadInput = lastControl.device is Gamepad;
                bool isKeyboardInput = lastControl.device is Keyboard;

                if (lastControl == null || (expectGamepad && !isGamepadInput) || (!expectGamepad && !isKeyboardInput))
                {
                    Debug.Log("Ignored input from wrong device type or no control selected.");
                    op.Dispose();
                    actionToRebind.Enable();
                    onComplete?.Invoke();
                    return;
                }

                SaveBindingOverride(actionToRebind, bindingIndex);
                actionToRebind.Enable();
                op.Dispose();
                onComplete?.Invoke();
            })
            .Start();
    }

    public void ResetKeybind(string actionName, int bindingIndex)
    {
        InputAction action = playerInput.actions[actionName];
        if (action == null)
            return;

        string defaultPath = action.bindings[bindingIndex].path;

        foreach (var otherAction in playerInput.actions)
        {
            if (otherAction.name == actionName)
                continue;

            for (int i = 0; i < otherAction.bindings.Count; i++)
            {
                string otherPath = otherAction.bindings[i].overridePath ?? otherAction.bindings[i].effectivePath;
                if (otherPath == defaultPath)
                {
                    string currentKey = action.bindings[bindingIndex].overridePath ?? action.bindings[bindingIndex].effectivePath;
                    AssignKeyToAction(otherAction.name, i, currentKey);
                }
            }
        }

        action.Disable();
        action.ApplyBindingOverride(bindingIndex, defaultPath);
        SaveBindingOverride(action, bindingIndex);
        action.Enable();
    }


    public void AssignKeyToAction(string actionName, int bindingIndex, string keyPath)
    {
        InputAction action = playerInput.actions[actionName];
        if (action == null)
        {
            Debug.LogWarning($"Action '{actionName}' not found!");
            return;
        }

        action.Disable();
        action.ApplyBindingOverride(bindingIndex, keyPath);
        SaveBindingOverride(action, bindingIndex);
        action.Enable();
    }

    public string GetBindingDisplayString(string actionName, int bindingIndex)
    {
        InputAction action = playerInput.actions[actionName];
        if (action != null && bindingIndex >= 0 && bindingIndex < action.bindings.Count)
            return action.GetBindingDisplayString(bindingIndex);
        return "";
    }

    public string GetBindingPath(string actionName, int bindingIndex)
    {
        InputAction action = playerInput.actions[actionName];
        if (action == null || bindingIndex < 0 || bindingIndex >= action.bindings.Count)
            return null;
        return action.bindings[bindingIndex].overridePath ?? action.bindings[bindingIndex].effectivePath;
    }

    private void SaveBindingOverride(InputAction action, int bindingIndex)
    {
        string key = $"binding_{action.name}_{bindingIndex}";
        string overridePath = action.bindings[bindingIndex].overridePath ?? string.Empty;
        PlayerPrefs.SetString(key, overridePath);
        PlayerPrefs.Save();
    }

    private void LoadBindingOverride(InputAction action, int bindingIndex)
    {
        string key = $"binding_{action.name}_{bindingIndex}";
        if (PlayerPrefs.HasKey(key))
        {
            string savedPath = PlayerPrefs.GetString(key);
            if (!string.IsNullOrEmpty(savedPath))
                action.ApplyBindingOverride(bindingIndex, savedPath);
        }
    }

    private void LoadAllBindings()
    {
        foreach (var action in playerInput.actions)
        {
            for (int i = 0; i < action.bindings.Count; i++)
                LoadBindingOverride(action, i);
        }
    }
}
