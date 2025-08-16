using UnityEngine;

public class StarFlip : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 120 *Time.deltaTime, 0);
    }
}
