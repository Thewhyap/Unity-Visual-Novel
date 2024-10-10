using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertsManager : MonoBehaviour
{
    public static AlertsManager Instance { get; private set; }

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

    public void ToggleQuitAlert()
    {
        Alert.Instance.Show(
            "Are you sure you want to quit?",
            "Quit",
            "Cancel",
            () => {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            },
            () => Alert.Instance.Close(),
            () => Alert.Instance.Close()
        );
    }

    public void ToggleSaveAlreadyExistsAlert(SaveData data, string saveSlot)
    {

        Alert.Instance.Show(
            $"Are you sure you want to overwrite this file?",
            "Overwrite",
            "Cancel",
            () => {
                SaveSystem.Save(data, saveSlot);

            },
            () => Alert.Instance.Close(),
            () => Alert.Instance.Close()
        );
    }

    public void ToggleDeleteSaveConfirmationAlert(string saveSlot)
    {
        Alert.Instance.Show(
            "Are you sure you want to delete this save?",
            "Delete",
            "Cancel",
            () => {
                SaveSystem.Delete(saveSlot);
            },
            () => Alert.Instance.Close(),
            () => Alert.Instance.Close()
        );
    }
}
