using System;
using System.Collections.Generic;
using UnityEngine;

// Creates itself automatically when the game starts. No scene setup needed.
// It loads every AchievementData file found in Assets/Resources/Achievements.
public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    private readonly List<AchievementData> achievements = new List<AchievementData>();
    public IReadOnlyList<AchievementData> All => achievements;

    // The popup listens to this
    public event Action<AchievementData> OnUnlocked;

    // Runs automatically before the first scene loads
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Bootstrap()
    {
        if (Instance != null) return;
        new GameObject("AchievementManager").AddComponent<AchievementManager>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Load all achievements and sort them by file name (so 01_, 02_, ... stay in order)
        achievements.AddRange(Resources.LoadAll<AchievementData>("Achievements"));
        achievements.Sort((a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
    }

    public bool IsUnlocked(string id) => PlayerPrefs.GetInt("ach_" + id, 0) == 1;

    // Call when the player opens a recipe
    public void ReportRecipeOpened()
    {
        foreach (var a in achievements)
        {
            if (a == null || IsUnlocked(a.id)) continue;
            if (a.type == AchievementType.FirstRecipeOpened) Unlock(a);
        }
        CheckCompleteAll();
    }

    // Call ONLY when a recipe was cooked successfully. secondsTaken = time from starting to finishing the meal.
    public void ReportRecipeCooked(string recipeId, float secondsTaken)
    {
        foreach (var a in achievements)
        {
            if (a == null || IsUnlocked(a.id)) continue;

            bool earned = false;
            switch (a.type)
            {
                case AchievementType.SpecificRecipe: earned = (recipeId == a.recipeId); break;
                case AchievementType.CookUnderTime: earned = secondsTaken < a.maxSeconds; break;
            }
            if (earned) Unlock(a);
        }
        CheckCompleteAll();
    }

    private void CheckCompleteAll()
    {
        foreach (var a in achievements)
        {
            if (a == null || a.type != AchievementType.CompleteAllOthers || IsUnlocked(a.id)) continue;

            bool all = true;
            foreach (var other in achievements)
            {
                if (other == null || other == a || other.type == AchievementType.CompleteAllOthers) continue;
                if (!IsUnlocked(other.id)) { all = false; break; }
            }
            if (all) Unlock(a);
        }
    }

    private void Unlock(AchievementData a)
    {
        PlayerPrefs.SetInt("ach_" + a.id, 1);
        PlayerPrefs.Save();
        OnUnlocked?.Invoke(a);
    }

    // Handy for testing
    public void ResetAll()
    {
        foreach (var a in achievements) PlayerPrefs.DeleteKey("ach_" + a.id);
        PlayerPrefs.Save();
    }
}