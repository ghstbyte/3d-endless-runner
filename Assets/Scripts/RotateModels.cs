using UnityEngine;

public class RotateModels : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 120 *Time.deltaTime, 0);
    }
}
