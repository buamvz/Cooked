
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DailyRecipeSelectorTests
{
    [Test]
    public void SelectsRecipeFromAvailableRecipes()
    {
        //create a list of recipes the player can access.
        List<string> availableRecipes = new List<string>
        {
            "Pancit",
            "Butter Chicken",
            "Katsudon"
        };

        //placeholder for the daily recipe selection.
        //We will replace this with the real selector implementation.
        string selectedRecipe = DailyRecipeSelector.SelectRandomRecipe(availableRecipes);

        //the selected recipe must be one of the available recipes.
        Assert.Contains(selectedRecipe, availableRecipes);
    }

    [Test]
    public void SelectsOnlyUnlockedRecipes()
    {
        //these are all recipes in the game.
        List<string> allRecipes = new List<string>
        {
            "Pancit",
            "Butter Chicken",
            "Katsudon"
        };

        //Only Pancit and Katsudon have been unlocked by the player
        List<string> unlockedRecipes = new List<string>
        {
            "Pancit",
            "Katsudon"
        };

        //select a recipe using the unlocked recipes
        string selectedRecipe = DailyRecipeSelector.SelectRandomRecipe(unlockedRecipes);

        //The selected recipe must not be locked.
        Assert.Contains(selectedRecipe, unlockedRecipes);
    }
    
    [Test]
    public void DailyRecipeRemainsTheSameWhenSceneIsReopened()
    {
        // Clear any previous daily selection so the test starts fresh.
        PlayerPrefs.DeleteKey("DailyRecipe");
        PlayerPrefs.DeleteKey("DailyRecipeDate");

        // Supply recipes that are available to the player.
        List<string> unlockedRecipes = new List<string>
        {
            "Pancit",
            "Butter Chicken",
            "Katsudon"
        };

        // Simulate opening the recipe scene for the first time today.
        string firstSelection =
            DailyRecipeSelector.GetDailyRecipe(unlockedRecipes);

        // Simulate reopening the recipe scene on the same day.
        string secondSelection =
            DailyRecipeSelector.GetDailyRecipe(unlockedRecipes);

        // Both calls should return the same saved daily recipe.
        Assert.AreEqual(firstSelection, secondSelection);

        // Confirm that a recipe was actually selected.
        Assert.IsNotNull(firstSelection);

        // Clean up saved values so this test does not affect other tests.
        PlayerPrefs.DeleteKey("DailyRecipe");
        PlayerPrefs.DeleteKey("DailyRecipeDate");
    }


}
