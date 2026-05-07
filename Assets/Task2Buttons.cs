using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Task2Buttons : MonoBehaviour
{
    [SerializeField] private Text messageText;

    private static Sprite cachedUiSprite;

    [ContextMenu("Create Task 2 Interface")]
    public void CreateTask2Interface()
    {
        EnsureEventSystem();
        ClearExistingInterface();
        Canvas canvas = CreateCanvas();
        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        GameObject panel = CreatePanel(canvas.transform);
        messageText = CreateText(panel.transform, "MessageText", new Vector2(0f, 120f), new Vector2(520f, 60f), font);
        messageText.text = "";

        CreateButton(panel.transform, "Next Scene", new Vector2(0f, 30f), new Vector2(240f, 55f), font, LoadNextScene);
        CreateButton(panel.transform, "Show Message", new Vector2(0f, -40f), new Vector2(240f, 55f), font, ShowMessage);
        CreateButton(panel.transform, "Quit", new Vector2(0f, -110f), new Vector2(240f, 55f), font, QuitGame);
    }

    private Canvas CreateCanvas()
    {
        Canvas existingCanvas = GetComponentInChildren<Canvas>(true);
        if (existingCanvas != null)
        {
            return existingCanvas;
        }

        GameObject canvasObject = new GameObject("Task2Canvas");
        canvasObject.transform.SetParent(transform, false);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObject.AddComponent<GraphicRaycaster>();

        return canvas;
    }

    private GameObject CreatePanel(Transform parent)
    {
        Transform existingPanel = parent.Find("Task2Panel");
        if (existingPanel != null)
        {
            return existingPanel.gameObject;
        }

        GameObject panelObject = new GameObject("Task2Panel");
        panelObject.transform.SetParent(parent, false);

        Image image = panelObject.AddComponent<Image>();
        image.color = new Color(0f, 0f, 0f, 0.35f);

        RectTransform rectTransform = panelObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(600f, 320f);
        rectTransform.anchoredPosition = Vector2.zero;

        return panelObject;
    }

    private void ClearExistingInterface()
    {
        Canvas existingCanvas = GetComponentInChildren<Canvas>(true);
        if (existingCanvas == null)
        {
            return;
        }

        Transform panel = existingCanvas.transform.Find("Task2Panel");
        if (panel != null)
        {
            if (Application.isPlaying)
            {
                Destroy(panel.gameObject);
            }
            else
            {
                DestroyImmediate(panel.gameObject);
            }
        }
    }

    private Text CreateText(Transform parent, string objectName, Vector2 anchoredPosition, Vector2 size, Font font)
    {
        GameObject textObject = new GameObject(objectName);
        textObject.transform.SetParent(parent, false);

        Text text = textObject.AddComponent<Text>();
        text.font = font;
        text.fontSize = 26;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        RectTransform rectTransform = text.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = anchoredPosition;

        return text;
    }

    private void CreateButton(Transform parent, string label, Vector2 anchoredPosition, Vector2 size, Font font, UnityEngine.Events.UnityAction onClickAction)
    {
        GameObject buttonObject = new GameObject(label + " Button");
        buttonObject.transform.SetParent(parent, false);

        Image image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.18f, 0.35f, 0.75f, 1f);
        image.sprite = GetOrCreateUiSprite();

        Button button = buttonObject.AddComponent<Button>();
        button.onClick.AddListener(onClickAction);

        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.pivot = new Vector2(0.5f, 0.5f);
        buttonRect.sizeDelta = size;
        buttonRect.anchoredPosition = anchoredPosition;

        GameObject textObject = new GameObject("Text");
        textObject.transform.SetParent(buttonObject.transform, false);

        Text buttonText = textObject.AddComponent<Text>();
        buttonText.font = font;
        buttonText.text = label;
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.color = Color.white;
        buttonText.resizeTextForBestFit = true;
        buttonText.resizeTextMinSize = 14;
        buttonText.resizeTextMaxSize = 28;

        RectTransform textRect = buttonText.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;
    }

    private Sprite GetOrCreateUiSprite()
    {
        if (cachedUiSprite != null)
        {
            return cachedUiSprite;
        }

        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.SetPixel(0, 0, Color.white);
        texture.SetPixel(1, 0, Color.white);
        texture.SetPixel(0, 1, Color.white);
        texture.SetPixel(1, 1, Color.white);
        texture.Apply();

        cachedUiSprite = Sprite.Create(texture, new Rect(0f, 0f, 2f, 2f), new Vector2(0.5f, 0.5f), 100f);
        cachedUiSprite.name = "GeneratedUiSprite";
        return cachedUiSprite;
    }

    private void EnsureEventSystem()
    {
        if (FindAnyObjectByType<EventSystem>() != null)
        {
            return;
        }

        GameObject eventSystemObject = new GameObject("EventSystem");
        eventSystemObject.AddComponent<EventSystem>();
        eventSystemObject.AddComponent<StandaloneInputModule>();
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ShowMessage()
    {
        if (messageText != null)
        {
            messageText.text = "Button clicked!";
            messageText.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Button clicked!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
