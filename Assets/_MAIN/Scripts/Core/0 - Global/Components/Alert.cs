using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class Alert : MonoBehaviour
{
    public static Alert Instance { get; private set; }

    public GameObject panel;
    public TextMeshProUGUI messageText;
    public Button yesButton;
    public Button noButton;
    public GameObject background;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
    }

    public void Close()
    {
        Instance.gameObject.SetActive(false);
    }
}
