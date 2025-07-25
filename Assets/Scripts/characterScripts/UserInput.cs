using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput instance;

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
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _dashAction;
    private InputAction _pauseAction;
    private InputAction _abilityOneAction;


    private PlayerInput playerInput;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        playerInput = gameObject.GetComponent<PlayerInput>();

        SetupInputActions();
    }

    void SetupInputActions()
    {
        _moveAction = playerInput.actions["Movement"];
        _jumpAction = playerInput.actions["Jump"];
        _attackAction = playerInput.actions["Attack"];
        _dashAction = playerInput.actions["Dash"];
        _pauseAction = playerInput.actions["Pause"];
        _abilityOneAction = playerInput.actions["AbilityOne"];
    }

    private void Update()
    {
        moveInput = _moveAction.ReadValue<Vector2>();
        jumpInput = _jumpAction.WasPressedThisFrame();
        attackInput = _attackAction.WasPressedThisFrame();
        dashInput = _dashAction.WasPressedThisFrame();
        pauseInput = _pauseAction.WasPressedThisFrame();
        abilityOneInput = _abilityOneAction.WasPressedThisFrame();
    }
}
