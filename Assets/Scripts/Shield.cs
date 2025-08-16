using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    private bool _isImmortalActive;
    public bool IsImmortalActive => _isImmortalActive;
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
        yield return new WaitForSeconds(5f);
        _isImmortalActive = false;
    }
}
