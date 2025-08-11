using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _speed;
    [SerializeField] private float _lineDistance;
    [SerializeField] private float _lineChangeSpeed;
    private int _currentLine = 0;
    private float _gravity = -9.81f;
    private float _jumpHeight = 2f;
    private CharacterController _characterController;
    private Vector3 _direction;
    private Vector3 _playerTargetPosition;
    private Vector3 _changeHorizontalVector;
    private Vector3 _changeVerticalVector;
    private Vector3 _totalMovementVector;

    private void OnEnable()
    {
        _inputManager.HorizontalMovement += ChangeLine;
        _inputManager.VerticalMovementUp += _onJumpRequested;
    }
    private void OnDisable()
    {
        _inputManager.HorizontalMovement -= ChangeLine;
        _inputManager.VerticalMovementUp -= _onJumpRequested;
    }

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTargetPosition = transform.position;
    }

    void Update()
    {
        _direction = Vector3.forward * _speed;
        _playerTargetPosition = new Vector3(_currentLine * _lineDistance, transform.position.y, transform.position.z);
        float newX = Mathf.MoveTowards(transform.position.x, _playerTargetPosition.x, _lineChangeSpeed * Time.deltaTime);
        _changeHorizontalVector = new Vector3(newX - transform.position.x, 0, _direction.z * Time.deltaTime);
        _totalMovementVector = _changeHorizontalVector + _changeVerticalVector * Time.deltaTime;
        _characterController.Move(_totalMovementVector);
        _changeVerticalVector.y += _gravity * Time.deltaTime;
    }

    private void ChangeLine(int direction)
    {
        _currentLine = Mathf.Clamp(_currentLine + direction, -1, 1);
    }

    private void _onJumpRequested (object inputJump, EventArgs e)
    {
        if (_characterController.isGrounded == true)
        {
            _changeVerticalVector.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }
}
