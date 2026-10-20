using UnityEngine;

// Put this on an ALWAYS-ACTIVE object (not on the popup itself).
// Settings "Achievements" button -> Open(), popup's Close button -> Close()
public class AchievementsMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;        // the popup panel that gets shown/hidden
    [SerializeField] private Transform container;     // the Scroll View's Content object
    [SerializeField] private AchievementSlot slotPrefab;

    public void Open()
    {
        panel.SetActive(true);
        Refresh();
    }

    public void Close() => panel.SetActive(false);

    private void Refresh()
    {
        foreach (Transform child in container) Destroy(child.gameObject);

        var manager = AchievementManager.Instance;
        if (manager == null) return;

        foreach (var a in manager.All)
        {
            var slot = Instantiate(slotPrefab, container);
            slot.Setup(a, manager.IsUnlocked(a.id));
        }
    }
}