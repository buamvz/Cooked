using UnityEngine;

public enum AchievementType
{
    FirstRecipeOpened,  // player opens any recipe for the first time
    SpecificRecipe,     // first successful cook of one particular recipe (set Recipe Id)
    CookUnderTime,      // successful cook faster than Max Seconds
    CompleteAllOthers   // every other achievement is unlocked
}

// Create via: Right-click in Project > Create > Cooked > Achievement
[CreateAssetMenu(fileName = "Achievement_", menuName = "Cooked/Achievement")]
public class AchievementData : ScriptableObject
{
    public string id;               // unique, e.g. "pancit_pro"
    public string title;            // e.g. "Pancit Pro"
    [TextArea] public string description;
    public Sprite icon;

    public AchievementType type;
    public string recipeId;         // only for SpecificRecipe
    public float maxSeconds;        // only for CookUnderTime (300 = 5 min, 180 = 3 min)
}