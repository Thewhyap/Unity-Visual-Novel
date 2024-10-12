using UnityEngine.SceneManagement;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private SaveData currentSaveData = null;
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

    public SaveData GetCurrentSaveData(bool dataWillBeUpdated = true)
    {
        if(dataWillBeUpdated) isCurrentDataSaved = false;
        return currentSaveData;
    }

    public void SetCurrentSaveData(SaveData data)
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

    public async void LoadSave(string saveSlot)
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

    public async void DeleteSave(string saveSlot)
    {
        bool shouldDelete = await AlertsManager.Instance.ToggleDeleteSaveConfirmationAlert();
        if (shouldDelete)
        {
            isCurrentDataSaved = false;
            SaveSystem.Delete(saveSlot);
        }
    }

    public async void Save(string saveSlot)
    {
        bool shouldSave = true;
        if (SaveSystem.IsSaveExisting(saveSlot)) shouldSave = await AlertsManager.Instance.ToggleSaveAlreadyExistsAlert();
        if(shouldSave)
        {
            isCurrentDataSaved = true;
            SaveSystem.Save(currentSaveData, saveSlot);
        }
    }

    public void RenameSave(string saveSlot, string newName)
    {
        SaveData data = SaveSystem.Load(saveSlot);
        data.saveName = newName;
        SaveSystem.Save(data, saveSlot);
    }
}
