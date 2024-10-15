using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public List<GameObject> layersToHideForScreenshot;

    void Start()
    {
        ScreenshotManager.Instance.layersToHide = layersToHideForScreenshot;
    }
}
