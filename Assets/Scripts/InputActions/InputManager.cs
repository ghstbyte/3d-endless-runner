using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Movement movement;
    public Vector2 _inputActions;
    public event Action<int> EventButton;

    private void Awake()
    {
        movement = new Movement();
        movement.Enable();
    }

    private void Update()
    {
        _buttonPressed();
    }

    private void _buttonPressed()
    {
        _inputActions = movement.Keyboard.Actions.ReadValue<Vector2>();
        if (movement.Keyboard.Actions.WasPerformedThisFrame() && _inputActions.x > 0)
        {
            EventButton?.Invoke(+1);
        }
        else if (movement.Keyboard.Actions.WasPerformedThisFrame() && _inputActions.x < 0)
        {
            EventButton?.Invoke(-1);
        }
    }

}
