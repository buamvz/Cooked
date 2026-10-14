using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DailyRecipeUI : MonoBehaviour
{
    // The TMP text that displays today's selected recipe.
    [SerializeField] private TMP_Text dailyRecipeName;

    // The button that opens the selected recipe's cooking scene.
    [SerializeField] private Button cookRecipeButton;

    // The button that returns the player to the Level Map.
    [SerializeField] private Button backToLevelMapButton;

    // Recipe names and their corresponding Unity scene names.
    // Update these scene names to match your actual cooking scenes.
    [System.Serializable]
    public class RecipeScene
    {
        public string recipeName;
        public string sceneName;
    }

    [SerializeField] private List<RecipeScene> recipes = new List<RecipeScene>();

    // Stores the recipe selected for this screen.
    private string selectedRecipe;

    private void Start()
    {
        // Build a list of recipe names from the Inspector configuration.
        List<string> allRecipes = new List<string>();

        foreach (RecipeScene recipe in recipes)
        {
            if (recipe != null && !string.IsNullOrEmpty(recipe.recipeName))
            {
                allRecipes.Add(recipe.recipeName);
            }
        }

        // TEMPORARY: treat every configured recipe as unlocked.
        // We'll replace this with the game's actual unlock data next.
        List<string> unlockedRecipes = new List<string>(allRecipes);

        // Select today's recipe using the tested daily selector.
        selectedRecipe = DailyRecipeSelector.GetDailyRecipe(unlockedRecipes);

        // Display the selected recipe, or a helpful message if none exist.
        if (dailyRecipeName != null)
        {
            dailyRecipeName.text = selectedRecipe ?? "No recipes available";
        }

        // Connect the buttons to their actions.
        if (cookRecipeButton != null)
        {
            cookRecipeButton.onClick.AddListener(OpenSelectedRecipe);
            cookRecipeButton.interactable = !string.IsNullOrEmpty(selectedRecipe);
        }

        if (backToLevelMapButton != null)
        {
            backToLevelMapButton.onClick.AddListener(BackToLevelMap);
        }
    }

    private void OpenSelectedRecipe()
    {
        // Find the Unity scene that corresponds to today's recipe.
        foreach (RecipeScene recipe in recipes)
        {
            if (recipe != null && recipe.recipeName == selectedRecipe)
            {
                // Load the configured cooking scene.
                SceneManager.LoadScene(recipe.sceneName);
                return;
            }
        }

        Debug.LogWarning("No cooking scene is configured for today's recipe.");
    }

    private void BackToLevelMap()
    {
        // Return to the existing Level Map scene.
        SceneManager.LoadScene("Level Map");
    }

    private void OnDestroy()
    {
        // Remove listeners so they aren't left attached after this screen closes.
        if (cookRecipeButton != null)
        {
            cookRecipeButton.onClick.RemoveListener(OpenSelectedRecipe);
        }

        if (backToLevelMapButton != null)
        {
            backToLevelMapButton.onClick.RemoveListener(BackToLevelMap);
        }
    }
}