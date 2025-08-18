using UnityEngine;

public class MobileOptimizer : MonoBehaviour
{
    private int originalWidth;
    private int originalHeight;
    private bool resolutionSet = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        originalWidth = Screen.currentResolution.width;
        originalHeight = Screen.currentResolution.height;
    }

    void Start()
    {
        if (!resolutionSet)
        {
            Screen.SetResolution(originalWidth / 2, originalHeight / 2, true);
            resolutionSet = true;
        }
    }
}
