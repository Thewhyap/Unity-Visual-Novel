using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlertsManager.Instance.ToggleQuitAlert();
        }
    }
}
