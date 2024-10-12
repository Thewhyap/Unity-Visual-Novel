using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class Alert : MonoBehaviour
{
    public static Alert Instance { get; private set; }

    public GameObject background;
    public GameObject panel;
    public TextMeshProUGUI messageText;
    public Button yesButton;
    public Button noButton;

    private const string MAIN_CANVAS = "Canvas - Main";
    private Transform alert;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            alert = transform.GetChild(0);
            Close();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Show(string message, string yesText = "Yes", string noText = "No", UnityAction yesAction = null, UnityAction noAction = null, UnityAction backgroundClickAction = null)
    {
        if (Instance.gameObject.activeSelf) return;

        GameObject mainCanvas = GameObject.Find(MAIN_CANVAS);
        if (mainCanvas != null) Reparent(mainCanvas.transform);
        else Debug.LogError($"'{MAIN_CANVAS}' not found!");

        messageText.text = message;
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        background.GetComponent<Button>().onClick.RemoveAllListeners();

        if (yesAction != null)
        {
            yesButton.onClick.AddListener(yesAction);
            yesButton.gameObject.SetActive(true);
            yesButton.GetComponentInChildren<TextMeshProUGUI>().text = yesText;
        }
        else
        {
            yesButton.gameObject.SetActive(false);
        }

        if (noAction != null)
        {
            noButton.onClick.AddListener(noAction);
            noButton.gameObject.SetActive(true);
            noButton.GetComponentInChildren<TextMeshProUGUI>().text = noText;
        }
        else
        {
            noButton.gameObject.SetActive(false);
        }

        if (backgroundClickAction != null)
        {
            background.GetComponent<Button>().onClick.AddListener(backgroundClickAction);
        }

        Instance.gameObject.SetActive(true);
        Instance.transform.SetAsLastSibling();
    }

    public void Close()
    {
        Reparent(transform);
        Instance.gameObject.SetActive(false);
    }

    private void Reparent(Transform newParent)
    {
        Vector3 localPosition = alert.localPosition;
        Vector3 localScale = alert.localScale;

        alert.SetParent(newParent, true);

        alert.localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z);
        alert.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
    }
}
