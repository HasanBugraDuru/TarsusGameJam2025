
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static PlayerInput PlayerInput;

    public static Vector2 Movement;
    public static Vector2 Shoot;
    public static bool PauseWasPressed;
    public static bool SelectWasPressed;
 

    private InputAction _moveAction;
    private InputAction _shootAction;
    private InputAction _PauseAction;
    private InputAction _SelectAction;

    private void Awake()
   {
        PlayerInput = GetComponent<PlayerInput>();
        _moveAction = PlayerInput.actions["Movement"];
        _shootAction = PlayerInput.actions["Shoot"];
        _SelectAction = PlayerInput.actions["Select"];
        _PauseAction = PlayerInput.actions["Pause"];

    }
    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        Shoot = _shootAction.ReadValue<Vector2>();
        SelectWasPressed = _SelectAction.WasPressedThisFrame();
        PauseWasPressed = _PauseAction.WasPressedThisFrame();
    }
}
