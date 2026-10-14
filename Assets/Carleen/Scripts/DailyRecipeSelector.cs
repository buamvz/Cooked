
using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyRecipeSelector
{
    // Keys used to save the daily recipe and its selection date.
    private const string DailyRecipeKey = "DailyRecipe";
    private const string DailyRecipeDateKey = "DailyRecipeDate";

    // Selects one random recipe from the supplied list.
    public static string SelectRandomRecipe(List<string> availableRecipes)
    {
        // Return null if there are no recipes to select.
        if (availableRecipes == null || availableRecipes.Count == 0)
        {
            return null;
        }

        // Choose a valid random index from the recipe list.
        int randomIndex = UnityEngine.Random.Range(0, availableRecipes.Count);

        // Return the recipe at that index.
        return availableRecipes[randomIndex];
    }

    // Gets today's daily recipe using the actual current date.
    public static string GetDailyRecipe(List<string> unlockedRecipes)
    {
        // Format today's date consistently.
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        // Reuse the date-aware method with today's date.
        return GetDailyRecipeForDate(unlockedRecipes, today);
    }

    // Gets the daily recipe for a specified date.
    // This method also allows us to test different dates without changing
    // the computer's system clock.
    public static string GetDailyRecipeForDate(
        List<string> unlockedRecipes, string requestedDate)
    {
        // Return null if there are no unlocked recipes or no date supplied.
        if (unlockedRecipes == null || unlockedRecipes.Count == 0 ||
            string.IsNullOrEmpty(requestedDate))
        {
            return null;
        }

        // Read the previously saved date and recipe.
        string savedDate = PlayerPrefs.GetString(DailyRecipeDateKey, "");
        string savedRecipe = PlayerPrefs.GetString(DailyRecipeKey, "");

        // Reuse the saved recipe if it belongs to this date
        // and the player still has it unlocked.
        if (savedDate == requestedDate &&
            unlockedRecipes.Contains(savedRecipe))
        {
            return savedRecipe;
        }

        // Select a recipe when there is no valid selection for this date.
        string selectedRecipe = SelectRandomRecipe(unlockedRecipes);

        // Save the selected recipe and requested date.
        PlayerPrefs.SetString(DailyRecipeKey, selectedRecipe);
        PlayerPrefs.SetString(DailyRecipeDateKey, requestedDate);
        PlayerPrefs.Save();

        // Return the selected daily recipe.
        return selectedRecipe;
    }



    // Selects a random recipe from all recipes that the player has unlocked.
    public static string SelectRandomUnlockedRecipe(
        List<string> allRecipes, List<string> unlockedRecipes)
    {
        // Return null if either list is missing or empty.
        if (allRecipes == null || unlockedRecipes == null ||
            allRecipes.Count == 0 || unlockedRecipes.Count == 0)
        {
            return null;
        }

        // Build a list containing only recipes that are both
        // in the game and unlocked by the player.
        List<string> eligibleRecipes = new List<string>();

        foreach (string recipe in allRecipes)
        {
            // Only add a recipe if it is unlocked and not already
            // included in the eligible list.
            if (unlockedRecipes.Contains(recipe) &&
                !eligibleRecipes.Contains(recipe))
            {
                eligibleRecipes.Add(recipe);
            }
        }

        // Select randomly from the eligible recipes only.
        return SelectRandomRecipe(eligibleRecipes);
    }


}
