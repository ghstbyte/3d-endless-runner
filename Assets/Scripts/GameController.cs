using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCharacterController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _endGameUI;
    [SerializeField] private Score _scoreScript;
    [SerializeField] private Shield _shieldScript;
    [SerializeField] private TMP_Text _recordScoreText;
    [SerializeField] private Coins coinsScript;
    private int _coins = 0;
    private int _totalCoins = 0;
    public TextMeshProUGUI _coinsText;

    private void Start()
    {
        int lastRunScore = PlayerPrefs.GetInt("lastRunScore");
        int recordScore = PlayerPrefs.GetInt("recordScore");
        if (recordScore < lastRunScore)
        {
            recordScore = lastRunScore;
            PlayerPrefs.SetInt("recordScore", recordScore);
            PlayerPrefs.Save();
            _recordScoreText.text = $"Record: {recordScore.ToString()}";
        }
        else
        {
            _recordScoreText.text = $"Record: {recordScore.ToString()}";
        }
    }

    private void Awake()
    {
        _playerCharacterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            if (_shieldScript.IsImmortalActive)
            {
                Destroy(hit.gameObject);
            }
            else
            {  
            _endGameUI.SetActive(true);
            SaveCoins(_coins);
            int _lastRunScore = int.Parse(_scoreScript._scoreText.text);
            PlayerPrefs.SetInt("lastRunScore", _lastRunScore);
            Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider additions)
    {
        if (additions.gameObject.CompareTag("Coins"))
        {
            _coins++;
            Destroy(additions.gameObject);
            coinsScript.updateCoinPanel(_coins);
            _coinsText.text = $"{_coins}";
        }
        if (additions.gameObject.CompareTag("BonusStar"))
        {
            _scoreScript.ActiveBonus();
            Destroy(additions.gameObject);
        }
        if (additions.gameObject.CompareTag("BonusShield"))
        {
            _shieldScript.ActiveShield();
            Destroy(additions.gameObject);
        }
    }

    private void SaveCoins(int coinsThisRun)
    {
        _totalCoins = PlayerPrefs.GetInt("Coins");
        _totalCoins += coinsThisRun;
        PlayerPrefs.SetInt("Coins", _totalCoins);
        PlayerPrefs.Save();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
