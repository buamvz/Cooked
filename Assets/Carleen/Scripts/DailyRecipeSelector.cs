using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyRecipeSelector
{
    //Stores the date and recipe for the current daily selection
    private const string DailyRecipeKey = "DailyRecipe";
    private const string DailyRecipeDateKey = "DailyRecipeDate";

    // Selects one recipe randomly from the recipes provided.
    public static string SelectRandomRecipe(List<string> availableRecipes)
    {
        // Return null if there are no recipes to choose from.
        if (availableRecipes == null || availableRecipes.Count == 0)
        {
            return null;
        }

        // Generate a random index within the list's valid range.
        int randomIndex = UnityEngine.Random.Range(0, availableRecipes.Count);

        // Return the recipe at that random position.
        return availableRecipes[randomIndex];
    }

    //Returns today's saved recipe, or selects and saves a new one
    public static string GetDailyRecipe(List<string> unlockedRecipes)
    {
        //There is no valid selection if no recipes are unlocked
        if(unlockedRecipes == null || unlockedRecipes.Count == 0)
        {
            return null;
        }

        //Use the current date as the daily identifier
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        //Read the saved date and recipe from PlayerPrefs
        string savedDate = PlayerPrefs.GetString(DailyRecipeDateKey, "");
        string savedRecipe = PlayerPrefs.GetString(DailyRecipeKey, "");

        //Reuse today's recipe if it is still unlocked
        if(savedDate == today && unlockedRecipes.Contains(savedRecipe))
        {
            return savedRecipe;
        }

        //Otherwise, choose a new recipe from the unlocked recipes
        string selectedRecipe = SelectRandomRecipe(unlockedRecipes);

        //save the recipe and the date so it can be reused today
        PlayerPrefs.SetString(DailyRecipeKey, selectedRecipe);
        PlayerPrefs.SetString(DailyRecipeDateKey, today);
        PlayerPrefs.Save();

        //Return the newly selected daily recipe
        return selectedRecipe;
    }
}
