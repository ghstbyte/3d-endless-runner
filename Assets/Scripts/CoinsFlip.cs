using UnityEngine;

public class CoinsFlip : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 120 *Time.deltaTime, 0);
    }
}
