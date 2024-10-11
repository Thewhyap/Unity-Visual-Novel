using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    public class MainMenuConfig
    {
        public string background;
    }

    private MainMenuConfig mainMenuConfig = new();
    private const string mainMenuConfigFilePath = "";

    void Start()
    {
        LoadMainMenuConfig();
    }

    void LoadMainMenuConfig()
    {
        GraphicPanel panel = GraphicPanelManager.Instance.GetPanel("Background");
        GraphicLayer layer0 = panel.GetLayer(0, true);

        if (File.Exists(mainMenuConfigFilePath))
        {
            string json = File.ReadAllText(mainMenuConfigFilePath);
            mainMenuConfig = JsonUtility.FromJson<MainMenuConfig>(json);
        }

        //VideoClip clip = Resources.Load<VideoClip>(mainMenuConfig.background);
        VideoClip clip = Resources.Load<VideoClip>("Graphics/BG Videos/Fantasy Landscape");
        if (clip == null)
        {
            Texture texture = Resources.Load<Texture2D>(mainMenuConfig.background);

            if (texture == null)
            {
                Debug.LogError($"Could not load graphic texture or video from path '{mainMenuConfig.background}'.");
                return;
            }
            layer0.SetTexture(texture);
        }
        layer0.SetVideo(clip, useAudio: true); //TODO let the possibility to choose if audio is true or false
    }
}
