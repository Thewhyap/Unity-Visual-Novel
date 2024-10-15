using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static string PreviousScene { get; private set; }
    public static string DefaultTab { get; private set; }

    public static void OpenSettings(string defaultTab = "savesPanel")
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        DefaultTab = defaultTab;

        ScreenshotManager.Instance.TakeScreenshot("lastScreenshot"); //TODO

        SceneManager.LoadScene("SettingsScene");
    }
}
