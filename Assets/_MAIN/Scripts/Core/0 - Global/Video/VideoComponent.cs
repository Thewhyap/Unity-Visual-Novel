using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoComponent : MonoBehaviour
{
    public string videoPath = string.Empty;
    public bool isLooping = false;
    public bool canSkip = false;
    public float skipDelay = 3f;

    public void Play()
    {
        if(canSkip && skipDelay > 0) StartCoroutine(EnableSkipAfterDelay(skipDelay));
    }

    private IEnumerator EnableSkipAfterDelay(float delay)
    {
        canSkip = false;
        yield return new WaitForSeconds(delay);
        canSkip = true;
    }
}