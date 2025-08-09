using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _speed;
    [SerializeField] private float _lineDistance;
    [SerializeField] private float _lineChangeSpeed;
    private int _currentLine = 0;
    private CharacterController _characterController;
    private Vector3 _direction;
    private Vector3 _playerTargetPosition;
    private Vector3 _moveVector;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerTargetPosition = transform.position;
    }

    void FixedUpdate()
    {
        _direction.z = _speed;
        _characterController.Move(_direction);
        MoveToCurrentLine();
    }

    void Update()
    {
        ChangePlayerPosition();
    }

    private void ChangePlayerPosition()
    {
        if (_inputManager._inputActions.x > 0)
        {
            ChangeLine(+1);
        }
        else if (_inputManager._inputActions.x < 0)
        {
            ChangeLine(-1);
        }
    }

    private void ChangeLine(int direction)
    {
        _currentLine = Mathf.Clamp(_currentLine + direction, -1, 1);
        MoveToCurrentLine();
    }

    private void MoveToCurrentLine()
    {
        _playerTargetPosition.x = _currentLine * _lineDistance;
        _moveVector = _playerTargetPosition - transform.position;
        _characterController.Move(_moveVector * Time.deltaTime);
    }
}
