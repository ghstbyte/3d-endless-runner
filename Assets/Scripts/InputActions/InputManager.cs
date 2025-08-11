using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Movement movement;
    private Vector2 _keyboardInputActions;
    private Vector2 _touchStartedPosition;
    private Vector2 _touchCanceledPosition;
    public event Action<int> LeftRightMovement;
    public event Action<int> JumpUpDown;

    private void Awake()
    {
        movement = new Movement();
            movement.Touchscreen.TouchPress.started += ctx =>
            {
                _touchStartedPosition = movement.Touchscreen.TouchPosition.ReadValue<Vector2>();
            };
                movement.Touchscreen.TouchPress.canceled += ctx =>
            {
                _touchCanceledPosition = movement.Touchscreen.TouchPosition.ReadValue<Vector2>();
                    Vector2 delta = _touchCanceledPosition - _touchStartedPosition;
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x > 0) LeftRightMovement?.Invoke(+1);
                    else LeftRightMovement?.Invoke(-1);
                }
                else
                {
                    if (delta.y > 0) Debug.Log("JumpUpDown?.Invoke(+1)");
                    else Debug.Log("JumpUpDown?.Invoke(-1)");
                }
            };
        movement.Enable();
    }

    private void Update()
    {
        _buttonPressed();
    }

    private void _buttonPressed()
    {
        _keyboardActive();
    }

    private void _keyboardActive()
    {
        _keyboardInputActions = movement.Keyboard.Actions.ReadValue<Vector2>();
        if (movement.Keyboard.Actions.WasPerformedThisFrame() && _keyboardInputActions.x > 0)
        {
            LeftRightMovement?.Invoke(+1);
        }
        else if (movement.Keyboard.Actions.WasPerformedThisFrame() && _keyboardInputActions.x < 0)
        {
            LeftRightMovement?.Invoke(-1);
        }
    }
}
