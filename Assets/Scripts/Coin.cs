using UnityEngine;

public class Coin : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 120 *Time.deltaTime, 0);
    }
}
