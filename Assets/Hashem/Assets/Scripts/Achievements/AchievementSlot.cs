using TMPro;
using UnityEngine;
using UnityEngine.UI;

// On the slot prefab: one icon Image, a title TMP_Text, a description TMP_Text
public class AchievementSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    private static readonly Color LockedTint = new Color(0.25f, 0.25f, 0.25f, 1f);
    private static readonly Color LockedText = new Color(1f, 1f, 1f, 0.4f);

    public void Setup(AchievementData data, bool unlocked)
    {
        icon.sprite = data.icon;
        titleText.text = data.title;
        descriptionText.text = data.description;

        // Greyed out until achieved
        icon.color = unlocked ? Color.white : LockedTint;
        titleText.color = unlocked ? Color.white : LockedText;
        descriptionText.color = unlocked ? Color.white : LockedText;
    }
}