using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;
    [SerializeField] private Transform _playerPosition;
    private float score = 0f;
    private void Update()
    {
        _updateScore();
    }
    private void _updateScore()
    {
        score = Mathf.Round(_playerPosition.position.z / 2);
        _scoreText.text =$"{score}";
    }
}
