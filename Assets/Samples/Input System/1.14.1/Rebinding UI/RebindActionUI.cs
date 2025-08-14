using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Samples.RebindUI
{
    public class RebindActionUI : MonoBehaviour
    {
        [Header("Action Setup")]
        public InputActionReference actionReference;
        public string bindingId;
        public InputBinding.DisplayStringOptions displayStringOptions;

        [Header("UI Elements")]
        public TMP_Text actionLabel;
        public TMP_Text bindingText;
        public TMP_Text rebindPrompt;
        public GameObject rebindOverlay;

        [Header("Optional Gamepad Icon")]
        [Tooltip("UI Image to display gamepad icon for this binding.")]
        public Image bindingIconImage;

        [Header("Optional Input Actions for UI map")]
        public InputActionAsset defaultInputActions;

        [Tooltip("Event triggered when binding display updates.")]
        public UpdateBindingUIEvent updateBindingUIEvent;

        [Tooltip("Event triggered when rebind starts.")]
        public InteractiveRebindEvent rebindStartEvent;

        [Tooltip("Event triggered when rebind ends.")]
        public InteractiveRebindEvent rebindStopEvent;

        private InputActionMap uiInputActionMap;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;
        private static List<RebindActionUI> allRebindUIs = new List<RebindActionUI>();

        private void Awake()
        {
            LoadBinding();
            UpdateActionLabel();
            UpdateBindingDisplay();
        }

        private void OnEnable()
        {
            allRebindUIs.Add(this);
            InputSystem.onActionChange += OnActionChange;

            if (defaultInputActions != null)
                uiInputActionMap = defaultInputActions.FindActionMap("UI");
        }

        private void OnDisable()
        {
            rebindOperation?.Dispose();
            rebindOperation = null;

            allRebindUIs.Remove(this);
            if (allRebindUIs.Count == 0)
                InputSystem.onActionChange -= OnActionChange;
        }

        private void OnActionChange(object obj, InputActionChange change)
        {
            if (change != InputActionChange.BoundControlsChanged)
                return;

            var action = obj as InputAction;
            var actionMap = action?.actionMap ?? obj as InputActionMap;
            var actionAsset = actionMap?.asset ?? obj as InputActionAsset;

            foreach (var ui in allRebindUIs)
            {
                var referencedAction = ui.actionReference?.action;
                if (referencedAction == null)
                    continue;

                if (referencedAction == action ||
                    referencedAction.actionMap == actionMap ||
                    referencedAction.actionMap?.asset == actionAsset)
                {
                    ui.UpdateBindingDisplay();
                }
            }
        }

        public void UpdateActionLabel()
        {
            if (actionLabel != null)
            {
                actionLabel.text = actionReference?.action?.name ?? string.Empty;
            }
        }

        public void UpdateBindingDisplay()
        {
            var displayString = string.Empty;
            var deviceLayout = string.Empty;
            var controlPath = string.Empty;

            var action = actionReference?.action;
            if (action != null)
            {
                int bindingIndex = GetBindingIndex(action, bindingId);
                if (bindingIndex != -1)
                    displayString = action.GetBindingDisplayString(bindingIndex, out deviceLayout, out controlPath, displayStringOptions);
            }

            if (bindingText != null)
                bindingText.text = displayString;

            if (bindingIconImage != null)
            {
                Sprite icon = null;
                if (!string.IsNullOrEmpty(controlPath) && controlPath.Contains("Gamepad"))
                {
                    string controlName = controlPath.Split('/')[1];
                    icon = GamepadIconManager.GetIconForButton(controlName);
                }

                if (icon != null)
                {
                    bindingIconImage.sprite = icon;
                    bindingIconImage.enabled = true;
                }
                else
                {
                    bindingIconImage.enabled = false;
                }
            }

            updateBindingUIEvent?.Invoke(this, displayString, deviceLayout, controlPath);
        }

        public void StartInteractiveRebind()
        {
            if (!ResolveActionAndBinding(out var action, out int bindingIndex))
                return;

            if (action.bindings[bindingIndex].isComposite)
            {
                int firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < action.bindings.Count && action.bindings[firstPartIndex].isPartOfComposite)
                    PerformInteractiveRebind(action, firstPartIndex, true);
            }
            else
            {
                PerformInteractiveRebind(action, bindingIndex);
            }
        }

        private void PerformInteractiveRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
        {
            rebindOperation?.Cancel();

            void CleanUp()
            {
                rebindOperation?.Dispose();
                rebindOperation = null;
                action.actionMap.Enable();
                uiInputActionMap?.Enable();
            }

            action.Disable();
            action.actionMap.Disable();
            uiInputActionMap?.Disable();

            rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
                .OnCancel(op =>
                {
                    action.Enable();
                    rebindStopEvent?.Invoke(this, op);
                    rebindOverlay?.SetActive(false);
                    UpdateBindingDisplay();
                    CleanUp();
                })
                .OnComplete(op =>
                {
                    action.Enable();
                    rebindOverlay?.SetActive(false);
                    rebindStopEvent?.Invoke(this, op);

                    SaveBinding();

                    UpdateBindingDisplay();
                    CleanUp();

                    if (allCompositeParts)
                    {
                        int nextIndex = bindingIndex + 1;
                        if (nextIndex < action.bindings.Count && action.bindings[nextIndex].isPartOfComposite)
                            PerformInteractiveRebind(action, nextIndex, true);
                    }
                });

            if (action.bindings[bindingIndex].isPartOfComposite)
            {
                string partName = $"Binding '{action.bindings[bindingIndex].name}'. ";
                if (rebindPrompt != null)
                    rebindPrompt.text = $"{partName}Waiting for input...";
            }
            else if (rebindPrompt != null)
            {
                rebindPrompt.text = "Waiting for input...";
            }

            rebindOverlay?.SetActive(true);
            rebindOperation.Start();
            rebindStartEvent?.Invoke(this, rebindOperation);
        }

        public bool ResolveActionAndBinding(out InputAction action, out int bindingIndex)
        {
            bindingIndex = -1;
            action = actionReference?.action;
            if (action == null || string.IsNullOrEmpty(bindingId))
                return false;

            bindingIndex = GetBindingIndex(action, bindingId);
            return bindingIndex != -1;
        }

        private int GetBindingIndex(InputAction action, string id)
        {
            var guid = new Guid(id);
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (action.bindings[i].id == guid)
                    return i;
            }
            return -1;
        }

        private void SaveBinding()
        {
            if (!ResolveActionAndBinding(out var action, out int bindingIndex))
                return;
            var overridePath = action.bindings[bindingIndex].overridePath;
            if (!string.IsNullOrEmpty(overridePath))
                PlayerPrefs.SetString(action.name + "_" + bindingId, overridePath);
            else
                PlayerPrefs.DeleteKey(action.name + "_" + bindingId);
            PlayerPrefs.Save();
        }

        private void LoadBinding()
        {
            if (!ResolveActionAndBinding(out var action, out int bindingIndex))
                return;
            string saved = PlayerPrefs.GetString(action.name + "_" + bindingId, null);
            if (!string.IsNullOrEmpty(saved))
                action.ApplyBindingOverride(bindingIndex, saved);
        }

        [Serializable]
        public class UpdateBindingUIEvent : UnityEvent<RebindActionUI, string, string, string>
        {
        }
        [Serializable]
        public class InteractiveRebindEvent : UnityEvent<RebindActionUI, InputActionRebindingExtensions.RebindingOperation>
        {
        }
    }

    public static class GamepadIconManager
    {
        public static Sprite GetIconForButton(string buttonName)
        {
            switch (buttonName)
            {
                case "buttonSouth":
                    return Resources.Load<Sprite>("Icons/Xbox_A");
                case "buttonEast":
                    return Resources.Load<Sprite>("Icons/Xbox_B");
                case "buttonWest":
                    return Resources.Load<Sprite>("Icons/Xbox_X");
                case "buttonNorth":
                    return Resources.Load<Sprite>("Icons/Xbox_Y");
                case "leftShoulder":
                    return Resources.Load<Sprite>("Icons/Xbox_LB");
                case "rightShoulder":
                    return Resources.Load<Sprite>("Icons/Xbox_RB");
                case "start":
                    return Resources.Load<Sprite>("Icons/Xbox_Start");
                case "select":
                    return Resources.Load<Sprite>("Icons/Xbox_Back");
                default:
                    return null;
            }
        }
    }
}
