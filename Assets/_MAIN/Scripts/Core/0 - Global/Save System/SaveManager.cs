using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private GameData currentSaveData = null;
    private bool isCurrentDataSaved = true;

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

    public GameData GetCurrentSaveData(bool dataWillBeUpdated = true)
    {
        if(dataWillBeUpdated) isCurrentDataSaved = false;
        return currentSaveData;
    }

    public void SetCurrentSaveData(GameData data)
    {
        isCurrentDataSaved = false;
        currentSaveData = data;
    }

    public async void BackToMainMenu()
    {
        bool shouldProcede = true;
        if (!isCurrentDataSaved) shouldProcede = await AlertsManager.Instance.ToggleUnsavedDataAlert();
        if (shouldProcede)
        {
            isCurrentDataSaved = true;
            currentSaveData = null;
            SceneManager.LoadScene("MainMenuScene");
        }   
    }

    public async void LoadSave(int saveSlot)
    {
        bool shouldProcede = true;
        if (!isCurrentDataSaved) shouldProcede = await AlertsManager.Instance.ToggleUnsavedDataAlert();
        if (shouldProcede)
        {
            isCurrentDataSaved = true;
            currentSaveData = null;
            SaveSystem.Load(saveSlot);
        }
    }

    public async void LoadLatest()
    {
        bool shouldProcede = true;
        if (!isCurrentDataSaved) shouldProcede = await AlertsManager.Instance.ToggleUnsavedDataAlert();
        if (shouldProcede)
        {
            isCurrentDataSaved = true;
            currentSaveData = null;
            SaveSystem.LoadLatest();
        }
    }

    public async void DeleteSave(int saveSlot)
    {
        bool shouldDelete = await AlertsManager.Instance.ToggleDeleteSaveConfirmationAlert();
        if (shouldDelete)
        {
            isCurrentDataSaved = false; //TODO not false if you delete something else than your current save
            SaveSystem.Delete(saveSlot);
        }
    }

    public async void Save(int saveSlot)
    {
        bool shouldSave = true;
        if (SaveSystem.IsSaveExisting(saveSlot)) shouldSave = await AlertsManager.Instance.ToggleSaveAlreadyExistsAlert();
        if(shouldSave)
        {
            isCurrentDataSaved = true;
            SaveSystem.Save(currentSaveData, saveSlot);
        }
    }

    public void RenameSave(int saveSlot, string newName)
    {
        HeaderData header = SaveSystem.GetHeader(saveSlot);
        header.saveName = newName;
        SaveSystem.SetHeader(header);
    }
}
