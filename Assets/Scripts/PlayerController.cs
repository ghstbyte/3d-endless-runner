using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _speed;
    [SerializeField] private float _lineDistance;
    [SerializeField] private float _lineChangeSpeed;
    private const float _maxSpeed = 100f;
    private int _currentLine = 0;
    private float _gravity = -9.81f;
    private float _jumpHeight = 3f;
    private CharacterController _characterController;
    private Vector3 _direction;
    private Vector3 _playerTargetPosition;
    private Vector3 _changeHorizontalVector;
    private Vector3 _changeVerticalVector;
    private Vector3 _totalMovementVector;

    private float _normalHeight = 2f;
    private Vector3 _normalCenter = new Vector3(0, 1f, 0);
    private float _slideHeight = 1f;
    private Vector3 _slideCenter = new Vector3(0, 0.5f, 0);
    private float _slideDuration = 1f;
    private bool _isSliding = false;

    private void Start()
    {
        StartCoroutine(_speedIncrease());
    }

    private void OnEnable()
    {
        _inputManager.HorizontalMovement += ChangeLine;
        _inputManager.VerticalMovementUp += _onJumpUpRequested;
        _inputManager.VerticalMovementDown += _onJumpDownRequested;
    }
    private void OnDisable()
    {
        _inputManager.HorizontalMovement -= ChangeLine;
        _inputManager.VerticalMovementUp -= _onJumpUpRequested;
        _inputManager.VerticalMovementDown -= _onJumpDownRequested;
    }

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTargetPosition = transform.position;
        _animator = GetComponent<Animator>();
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
        if (_characterController.isGrounded && !_isSliding)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    private void ChangeLine(int direction)
    {
        _currentLine = Mathf.Clamp(_currentLine + direction, -1, 1);
    }

    private void _onJumpUpRequested(object inputJumpUp, EventArgs e)
    {
        if (_characterController.isGrounded == true)
        {
            _changeVerticalVector.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            StartCoroutine(Jump());
        }
    }
    private void _onJumpDownRequested(object inputJumpDown, EventArgs e)
    {
        if (_characterController.isGrounded == false)
        {
            _changeVerticalVector.y = -Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            StartCoroutine(Slide());
        }
        if (_characterController.isGrounded == true && _isSliding == false)
        {
            StartCoroutine(Slide());
        }
    }

    private IEnumerator _speedIncrease()
    {
        while (_speed < _maxSpeed)
        {
            _speed += 1f;
            yield return new WaitForSeconds(2f);
        }
        StopCoroutine(_speedIncrease());
    }
    private IEnumerator Slide()
    {
        _isSliding = true;
        _animator.SetTrigger("Slide");
        _characterController.height = _slideHeight;
        _characterController.center = _slideCenter;
        yield return new WaitForSeconds(_slideDuration);
        _characterController.height = _normalHeight;
        _characterController.center = _normalCenter;
        _isSliding = false;
        _animator.ResetTrigger("Slide");
    }
    private IEnumerator Jump()
    {
        _animator.SetTrigger("Jump");
        yield return null;
        _animator.ResetTrigger("Jump");
    }
}
