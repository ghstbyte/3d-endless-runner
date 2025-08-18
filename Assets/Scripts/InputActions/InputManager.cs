using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerControllerScript;
    private Movement movement;
    private Vector2 _keyboardInputActions;
    private Vector2 _touchStartedPosition;
    private Vector2 _touchCanceledPosition;
    public event Action<int> HorizontalMovement;
    public event EventHandler VerticalMovementUp;
    public event EventHandler VerticalMovementDown;

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
                    if (delta.x > 0)
                    {
                        HorizontalMovement?.Invoke(+1);
                        _playerControllerScript.StartSlideRight();
                    }
                    else
                    {
                        HorizontalMovement?.Invoke(-1);
                        _playerControllerScript.StartSlideLeft();
                    }
                }
                else
                {
                    if (delta.y > 0) VerticalMovementUp?.Invoke(this, EventArgs.Empty);
                    else VerticalMovementDown?.Invoke(this, EventArgs.Empty);
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
            HorizontalMovement?.Invoke(+1);
            _playerControllerScript.StartSlideRight();
        }
        else if (movement.Keyboard.Actions.WasPerformedThisFrame() && _keyboardInputActions.x < 0)
        {
            HorizontalMovement?.Invoke(-1);
            _playerControllerScript.StartSlideLeft();
        }

        
        if (movement.Keyboard.Actions.WasPerformedThisFrame() && _keyboardInputActions.y > 0)
        {
            VerticalMovementUp?.Invoke(this, EventArgs.Empty);
        }
        else if (movement.Keyboard.Actions.WasPerformedThisFrame() && _keyboardInputActions.y < 0)
        {
            VerticalMovementDown?.Invoke(this, EventArgs.Empty);
        }
    }
}
