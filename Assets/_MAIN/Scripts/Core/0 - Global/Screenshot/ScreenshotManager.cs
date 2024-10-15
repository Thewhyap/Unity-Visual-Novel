using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    public static ScreenshotManager Instance { get; private set; }

    public List<GameObject> layersToHide;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeScreenshot(string filePath)
    {
        StartCoroutine(CaptureScreenshot(filePath));
    }

    private IEnumerator CaptureScreenshot(string filePath)
    {
        SetLayersVisibility(false);

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot(filePath);

        SetLayersVisibility(true);
    }

    private void SetLayersVisibility(bool visible)
    {
        foreach (GameObject layer in layersToHide) layer.SetActive(visible);
    }
}
