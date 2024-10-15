using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SavesController : MonoBehaviour
{
    private const int SLOTS_PER_PAGE = 8;
    private const int ROWS_PER_PAGE = 2;
    private const int PAGES_TO_SHOW_BEFORE = 7;
    private const int PAGES_TO_SHOW_AFTER = 1;

    public GameObject slotsContainer;
    public GameObject saveSlotPrefab;
    private GameObject[] slots = new GameObject[SLOTS_PER_PAGE];

    public GameObject pagesTab;
    public GameObject pageButtonPrefab;
    private GameObject[] pages = new GameObject[PAGES_TO_SHOW_BEFORE + PAGES_TO_SHOW_AFTER + 2];

    private int currentPage = 0;


    void Start()
    {
        CreateSlots();
        CreatePages();
        DisplayPage(currentPage);
    }

    private void CreateSlots()
    {
        int numberOfColumns = Mathf.CeilToInt((float)SLOTS_PER_PAGE / ROWS_PER_PAGE);

        float containerWidth = slotsContainer.GetComponent<RectTransform>().rect.width;
        float containerHeight = slotsContainer.GetComponent<RectTransform>().rect.height;

        float slotWidth = containerWidth / numberOfColumns;
        float slotHeight = containerHeight / ROWS_PER_PAGE;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(saveSlotPrefab);

            RectTransform slotRect = slots[i].GetComponent<RectTransform>();
            slotRect.sizeDelta = new Vector2(slotWidth, slotHeight);
            int row = i / numberOfColumns;
            int column = i % numberOfColumns;
            float xPos = (slotWidth * column) - (containerWidth / 2) + (slotWidth / 2);
            float yPos = (containerHeight / 2) - (slotHeight * row) - (slotHeight / 2);
            slotRect.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    private void CreatePages()
    {

        float containerWidth = pagesTab.GetComponent<RectTransform>().rect.width;
        float containerHeight = pagesTab.GetComponent<RectTransform>().rect.height;

        float pageNumberWidth = containerWidth;
        float pageNumberHeight = containerHeight / pages.Length;

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i] = Instantiate(pageButtonPrefab);

            RectTransform pageRect = pages[i].GetComponent<RectTransform>();
            pageRect.sizeDelta = new Vector2(pageNumberWidth, pageNumberHeight);
            int position = i / pages.Length;
            float xPos = (pageNumberWidth * position) - (containerWidth / 2) + (pageNumberWidth / 2);
            float yPos = (containerHeight / 2) - (pageNumberHeight / 2);
            pageRect.anchoredPosition = new Vector2(xPos, yPos);
        }
    }

    private void DisplayPage(int page)
    {
        UpdateSlots(page);
        UpdatePageNumbers(page);
    }

    private void UpdateSlots(int page)
    {
        List<HeaderData> headers = SaveSystem.headers;

        // pageNumber 0 is auto saves (slotNumbers -> {- SLOTS_PER_PAGE + 1 to 0})
        int startIndex = (page - 1) * SLOTS_PER_PAGE;
        for (int i = 1; i < SLOTS_PER_PAGE + 1; i++)
        {
            HeaderData data = headers.Find(header => header.slot == startIndex + i);
            updateSlotContent(i - 1, data);
        }
    }

    private void updateSlotContent(int slotNumber, HeaderData data)
    {
        GameObject background = slots[slotNumber].transform.Find("Background").gameObject;

        string color = "#444444";
        Sprite sprite = null;
        string saveName = "Empty Slot";
        string lastUpdate = null;
        string run = null;


        if(data != null)
        {
            color = data.color;
            //sprite = ;
            saveName = data.saveName;
            lastUpdate = data.lastUpdate.ToString();
            run = data.run.ToString();
        }

        if (ColorUtility.TryParseHtmlString(color, out Color newColor))
        {
            background.GetComponent<Image>().color = newColor;
        }
        else
        {
            Debug.LogError("Invalid hex color string: " + color);
        }

        //TODO set backgroundImage: background.transform.Find("Screenshot").GetComponent<Image>().sprite = sprite;

        background.transform.Find("Save Name").GetComponent<TextMeshProUGUI>().text = saveName;

        Transform lastUpdateTransform = background.transform.Find("Date Time");
        if (lastUpdate != null)
        {
            lastUpdateTransform.gameObject.SetActive(true);
            lastUpdateTransform.GetComponent<TextMeshProUGUI>().text = lastUpdate;
        }
        else lastUpdateTransform.gameObject.SetActive(false);

        Transform runTransform = background.transform.Find("Run");
        if (run != null)
        {
            runTransform.gameObject.SetActive(true);
            runTransform.GetComponent<TextMeshProUGUI>().text = run;
        }
        else runTransform.gameObject.SetActive(false);
    }

    private void UpdatePageNumbers(int currentPage)
    {
        for (int i = 1; i < pages.Length; i++)
        {
            Button button = pages[i].GetComponent<Button>();
            int pageNumber = currentPage - PAGES_TO_SHOW_BEFORE + i - 1;

            if (pageNumber < i) pageNumber = i;

            button.GetComponent<TextMeshProUGUI>().text = pageNumber.ToString();
        }
    }

    public void NextPage()
    {
        currentPage++;
        DisplayPage(currentPage);
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            DisplayPage(currentPage);
        }
    }

}
