using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public List<Tab> tabs;
    public Button mainMenuButton;
    public Button returnButton;

    private GameObject currentActivePanel;

    [System.Serializable]
    public class Tab
    {
        public Button tabButton;
        public GameObject tabContent;
    }

    void Start()
    {
        foreach (var tab in tabs)
        {
            if(!SettingsManager.DefaultTab.Equals(tab.tabContent.name)) tab.tabContent.SetActive(false);
            else currentActivePanel = tab.tabContent;

            if (SaveManager.Instance.GetCurrentSaveData(false) == null && tab.tabButton.name.Equals("History Button"))
                tab.tabButton.gameObject.SetActive(false);
            else tab.tabButton.onClick.AddListener(() => OpenTab(tab.tabContent));
        }

        if(SettingsManager.PreviousScene.Equals("MainMenuScene")) mainMenuButton.gameObject.SetActive(false);

        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenuScene"));
        returnButton.onClick.AddListener(() => ReturnToPreviousScene());
    }

    void OpenTab(GameObject panel)
    {
        currentActivePanel.SetActive(false);
        panel.SetActive(true);
        currentActivePanel = panel;
    }

    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(SettingsManager.PreviousScene);
    }
}
