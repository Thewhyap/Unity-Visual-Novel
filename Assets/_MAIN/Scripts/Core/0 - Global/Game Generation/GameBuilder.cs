using System.Collections;
using System.Collections.Generic;
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

    public void CreateGameWithData(SaveData data = null)
    {
        if (data == null)
        {
            //int run = GlobalData.Instance.IncreaseRunCount();
            data = new SaveData(0); //TODO use run instead of 0
        }

        //GlobalData.Instance.SetSaveData(data);
        if(SceneManager.GetActiveScene().name != "MainScene") SceneManager.LoadScene("MainScene");
    }
}
