using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

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

    public async Task<bool> ToggleSaveAlreadyExistsAlert()
    {
        TaskCompletionSource<bool> shouldSave = new();

        Alert.Instance.Show(
            $"Are you sure you want to overwrite this file?",
            "Overwrite",
            "Cancel",
            () => {
                shouldSave.SetResult(true);
                Alert.Instance.Close();
            },
            () => {
                shouldSave.SetResult(false);
                Alert.Instance.Close();
            },
            () => {
                shouldSave.SetResult(false);
                Alert.Instance.Close();
            }
        );

        return await shouldSave.Task;
    }

    public async Task<bool> ToggleDeleteSaveConfirmationAlert()
    {
        TaskCompletionSource<bool> shouldDelete = new();

        Alert.Instance.Show(
            "Are you sure you want to delete this save?",
            "Delete",
            "Cancel",
            () => {
                shouldDelete.SetResult(true);
                Alert.Instance.Close();
            },
            () => {
                shouldDelete.SetResult(false);
                Alert.Instance.Close();
            },
            () => {
                shouldDelete.SetResult(false);
                Alert.Instance.Close();
            }
        );

        return await shouldDelete.Task;
    }

    public async Task<bool> ToggleUnsavedDataAlert()
    {
        TaskCompletionSource<bool> shouldProcede = new();

        Alert.Instance.Show(
            "All unsaved progress will be lost, are you sure you want to quit?",
            "Yes",
            "Cancel",
            () => {
                shouldProcede.SetResult(true);
                Alert.Instance.Close();
            },
            () => {
                shouldProcede.SetResult(false);
                Alert.Instance.Close();
            },
            () => {
                shouldProcede.SetResult(false);
                Alert.Instance.Close();
            }
        );

        return await shouldProcede.Task;
    }
}
