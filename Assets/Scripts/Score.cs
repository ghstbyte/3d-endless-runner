using UnityEngine;
using TMPro;
using System.Collections;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _scoreBonusText;
    [SerializeField] private Transform _playerPosition;
    private float _score = 0f;
    private string _currentScore = "000000";
    private float _bonusTime = 30f;
    private bool _isBonusActive = false;
    private const float _scoreDivider = 2f;
    private const float _bonusDivider = 1f;
    private float _lastZ;
    private void Start()
    {
        _lastZ = _playerPosition.position.z;
    }
    private void Update()
    {
        _updateScore();
    }
    public void ActiveBonus()
    {
        if (!_isBonusActive)
        {
            StartCoroutine(BonusScore());
        }
    }
    private void _updateScore()
    {
        float currentZ = _playerPosition.position.z;
        float deltaZ = currentZ - _lastZ;

        if (deltaZ > 0)
        {
            if (_isBonusActive)
            {
                _score += deltaZ / _bonusDivider;
            }
            else
            {
                _score += deltaZ / _scoreDivider;
            }
        }
        _lastZ = currentZ;
        _scoreText.text = $"{Mathf.Round(_score)}";
        StartCoroutine(AnimatedScore(_score));
    }
    private IEnumerator BonusScore()
    {
        _isBonusActive = true;
        float bonusTimeActive = _bonusTime;

        while (bonusTimeActive > 0)
        {
            bonusTimeActive -= Time.deltaTime;
            _scoreBonusText.text = $"Bonus: {Mathf.Ceil(bonusTimeActive)}";
            yield return null;
        }
        _isBonusActive = false;
        _scoreBonusText.text = "";
    }
    private IEnumerator AnimatedScore(float targetScore)
    {
        int targetInt = Mathf.FloorToInt(targetScore);
        string target = targetInt.ToString("D6");

        for (int i = 0; i < target.Length; i++)
        {
            char[] temp = _currentScore.ToCharArray();
            temp[i] = target[i];
            _currentScore = new string(temp);
            _scoreText.text = _currentScore;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
