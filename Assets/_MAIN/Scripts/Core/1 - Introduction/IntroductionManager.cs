using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class IntroductionManager : MonoBehaviour
{
    public class IntroductionConfig
    {
        public List<string> videos;
    }

    private IntroductionConfig introductionConfig = new();
    private const string introConfigFilePath = "";
    private int currentVideoIndex = -1;
    private GraphicLayer mainLayer;

    void Start()
    {
        GraphicPanel panel = GraphicPanelManager.Instance.GetPanel("Main");
        mainLayer = panel.GetLayer(0, true);

        //LoadVideoConfig();
        //
        introductionConfig.videos = new List<string>();
        introductionConfig.videos.Add("Graphics/Logo/Apex_Baguette_Logo");
        //
        PlayNextVideo();
    }

    void LoadVideoConfig()
    {
        if (File.Exists(introConfigFilePath))
        {
            string json = File.ReadAllText(introConfigFilePath);
            introductionConfig = JsonUtility.FromJson<IntroductionConfig>(json);
        }
    }

    public void PlayNextVideo()
    {
        currentVideoIndex++;
        if (currentVideoIndex < introductionConfig.videos.Count)
        {
            mainLayer.SetVideo(introductionConfig.videos[currentVideoIndex], looping: false);
            StartCoroutine(OnVideoEnd());
        }
        else OnAllVideosFinished();
    }

    private IEnumerator OnVideoEnd()
    {
        yield return StartCoroutine(mainLayer.currentGraphic.Wait());
        PlayNextVideo();
    }

    void OnAllVideosFinished()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}