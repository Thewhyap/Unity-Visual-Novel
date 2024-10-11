using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

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

    public void LoadSave(string saveSlot)
    {
        GameBuilder.Instance.CreateGameWithData(SaveSystem.Load(saveSlot));
    }

    public void LoadLatest()
    {
        GameBuilder.Instance.CreateGameWithData(SaveSystem.LoadLatest());
    }

    public void DeleteSave(string saveSlot)
    {
        AlertsManager.Instance.ToggleDeleteSaveConfirmationAlert(saveSlot);
    }

    public void Save(SaveData data, string saveSlot)
    {
        if (SaveSystem.IsSaveExisting(saveSlot)) AlertsManager.Instance.ToggleSaveAlreadyExistsAlert(data, saveSlot);
        else SaveSystem.Save(data, saveSlot);
    }

    public void RenameSave(SaveData data, string newName)
    {
        data.saveName = newName;
    }
}
