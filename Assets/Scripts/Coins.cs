using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] RectTransform coinPanel;
    private float _stepWidth = 15f;

    public void updateCoinPanel(int coinValue)
    {
        int digitCount = coinValue.ToString().Length;
        int previousDigitCount = (coinValue - 1).ToString().Length;
        if (digitCount > previousDigitCount)
        {
            float currentWidth = coinPanel.sizeDelta.x;
            float targetWidth = currentWidth + _stepWidth;
            coinPanel.sizeDelta = new Vector2(targetWidth, coinPanel.sizeDelta.y);
        }
    }
}
