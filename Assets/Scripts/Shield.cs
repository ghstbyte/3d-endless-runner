using UnityEngine;
using System.Collections;
using TMPro;

public class Shield : MonoBehaviour
{
    [SerializeField] TMP_Text _shieldActiveTime;
    private bool _isImmortalActive;
    public bool IsImmortalActive => _isImmortalActive;
    private float _bonusTime = 5f;
    public void ActiveShield()
    {
        if (!_isImmortalActive)
        {
            StartCoroutine(BonusShield());
        }
    }
    private IEnumerator BonusShield()
    {
        _isImmortalActive = true;
        
        float _immortalActiveTime = _bonusTime;

        while (_immortalActiveTime > 0)
        {
            _immortalActiveTime -= Time.deltaTime;
            _shieldActiveTime.text = $"Bonus: {Mathf.Ceil(_immortalActiveTime)}";
            yield return null;
        }
        _isImmortalActive = false;
        _shieldActiveTime.text = "";
    }
}
