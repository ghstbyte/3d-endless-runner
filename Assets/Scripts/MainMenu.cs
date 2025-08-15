using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _totalCoinsText;
    private void Start()
    {
        int coins = PlayerPrefs.GetInt("Coins");
        _totalCoinsText.text = $"Coins: {coins.ToString()}";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
    }
}
