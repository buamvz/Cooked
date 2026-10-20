using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Builds its own UI in code and appears in every scene automatically.
// No Canvas, no prefab, no scene setup needed.
public class AchievementPopup : MonoBehaviour
{
    private const float ShowSeconds = 3f;
    private const float FadeSeconds = 0.3f;

    private static AchievementPopup instance;

    private CanvasGroup group;
    private Image icon;
    private Text label;
    private readonly Queue<AchievementData> queue = new Queue<AchievementData>();
    private bool showing;
    private bool subscribed;

    // Runs automatically before the first scene loads
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (instance != null) return;
        new GameObject("AchievementPopup").AddComponent<AchievementPopup>();
    }

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        BuildUI();
    }

    private void Start()
    {
        group.alpha = 0f;
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnUnlocked += Enqueue;
            subscribed = true;
        }
    }

    private void OnDestroy()
    {
        if (subscribed && AchievementManager.Instance != null)
            AchievementManager.Instance.OnUnlocked -= Enqueue;
    }

    private void BuildUI()
    {
        // Canvas that sits on top of everything
        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler));
        canvasGO.transform.SetParent(transform, false);
        var canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.matchWidthOrHeight = 0.5f;

        // Panel at the top centre
        var panel = new GameObject("Panel", typeof(RectTransform), typeof(Image), typeof(CanvasGroup));
        panel.transform.SetParent(canvasGO.transform, false);
        var rt = panel.GetComponent<RectTransform>();
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 1f);
        rt.pivot = new Vector2(0.5f, 1f);
        rt.sizeDelta = new Vector2(600f, 120f);
        rt.anchoredPosition = new Vector2(0f, -40f);

        var bg = panel.GetComponent<Image>();
        bg.color = new Color(0.1f, 0.1f, 0.1f, 0.92f);
        bg.raycastTarget = false;

        group = panel.GetComponent<CanvasGroup>();
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;   // never blocks taps on the game underneath

        // Icon (left)
        var iconGO = new GameObject("Icon", typeof(RectTransform), typeof(Image));
        iconGO.transform.SetParent(panel.transform, false);
        var iconRT = iconGO.GetComponent<RectTransform>();
        iconRT.anchorMin = iconRT.anchorMax = new Vector2(0f, 0.5f);
        iconRT.pivot = new Vector2(0f, 0.5f);
        iconRT.sizeDelta = new Vector2(80f, 80f);
        iconRT.anchoredPosition = new Vector2(20f, 0f);
        icon = iconGO.GetComponent<Image>();
        icon.preserveAspect = true;
        icon.raycastTarget = false;

        // Text (right of the icon)
        var textGO = new GameObject("Title", typeof(RectTransform), typeof(Text));
        textGO.transform.SetParent(panel.transform, false);
        var textRT = textGO.GetComponent<RectTransform>();
        textRT.anchorMin = Vector2.zero;
        textRT.anchorMax = Vector2.one;
        textRT.offsetMin = new Vector2(120f, 10f);
        textRT.offsetMax = new Vector2(-20f, -10f);
        label = textGO.GetComponent<Text>();
        label.font = Font.CreateDynamicFontFromOSFont(new[] { "Arial", "Roboto", "sans-serif" }, 28);
        label.fontSize = 28;
        label.color = Color.white;
        label.alignment = TextAnchor.MiddleLeft;
        label.raycastTarget = false;
    }

    private void Enqueue(AchievementData data)
    {
        queue.Enqueue(data);
        if (!showing) StartCoroutine(ShowQueue());
    }

    private IEnumerator ShowQueue()
    {
        showing = true;
        while (queue.Count > 0)
        {
            var data = queue.Dequeue();
            icon.sprite = data.icon;
            icon.enabled = data.icon != null;
            label.text = "Achievement unlocked!\n" + data.title;

            yield return Fade(0f, 1f);
            yield return new WaitForSecondsRealtime(ShowSeconds);
            yield return Fade(1f, 0f);
        }
        showing = false;
    }

    private IEnumerator Fade(float from, float to)
    {
        for (float t = 0; t < FadeSeconds; t += Time.unscaledDeltaTime)
        {
            group.alpha = Mathf.Lerp(from, to, t / FadeSeconds);
            yield return null;
        }
        group.alpha = to;
    }
}