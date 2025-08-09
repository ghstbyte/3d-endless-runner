using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Movement movement;
    public Vector2 _inputActions;

    private void Awake()
    {
        movement = new Movement();
        movement.Enable();
    }

    private void Update()
    {
        _inputActions = movement.Keyboard.Actions.ReadValue<Vector2>();
    }

}
