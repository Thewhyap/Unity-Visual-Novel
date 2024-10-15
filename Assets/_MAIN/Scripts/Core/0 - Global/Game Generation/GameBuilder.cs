using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBuilder : MonoBehaviour
{
    public static GameBuilder Instance { get; private set; }

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

    public void CreateGameWithData(GameData data = null)
    {
        if (data == null)
        {
            //int run = GlobalData.Instance.IncreaseRunCount();
            data = new GameData(0); //TODO use run instead of 0
        }

        //GlobalData.Instance.SetSaveData(data);
        if(SceneManager.GetActiveScene().name != "MainScene") SceneManager.LoadScene("MainScene");
    }
}
