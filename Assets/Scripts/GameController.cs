using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    [SerializeField] private CharacterController _playerCharacterController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _endGameUI;
    public TextMeshProUGUI _coinsText;
    private int _coins = 0;
    private int _totalCoins = 0;

    private void Awake()
    {
        _playerCharacterController = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            _endGameUI.SetActive(true);
            Time.timeScale = 0f;
            SaveCoins(_coins);
        }
    }

    private void OnTriggerEnter(Collider takeCoin)
    {
        if (takeCoin.gameObject.CompareTag("Coins"))
        {
            _coins++;
            Destroy(takeCoin.gameObject);
            _coinsText.text = $"Coins: {_coins}";
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
}
