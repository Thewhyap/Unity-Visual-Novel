using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void LoadMainMenu()
    {
        StartCoroutine(LoadSceneAsync("MainMenu"));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Active l'�cran de chargement
        loadingScreen.SetActive(true);

        // D�marre le chargement de la sc�ne en arri�re-plan
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            // Met � jour la barre de progression
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }
    }
}

