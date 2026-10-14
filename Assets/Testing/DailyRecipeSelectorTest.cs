
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
        // Arrange: create a list containing every recipe in the game.
        List<string> allRecipes = new List<string>
        {
            "Pancit",
            "Butter Chicken",
            "Katsudon"
        };

        // Only Pancit and Katsudon have been unlocked by the player.
        List<string> unlockedRecipes = new List<string>
        {
            "Pancit",
            "Katsudon"
        };

        // Act: ask the selector to choose from all recipes,
        // while considering which recipes the player has unlocked.
        string selectedRecipe = DailyRecipeSelector.SelectRandomUnlockedRecipe(allRecipes, unlockedRecipes);

        // Assert: a recipe must have been selected.
        Assert.IsNotNull(selectedRecipe);

        // The selected recipe must be one of the unlocked recipes.
        Assert.Contains(selectedRecipe, unlockedRecipes);

        // The locked recipe must never be selected.
        Assert.AreNotEqual("Butter Chicken", selectedRecipe);
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


    [Test]
    public void UpdatesSavedDateWhenNewDayIsRequested()
    {
        // Arrange: remove any saved daily recipe from earlier tests.
        PlayerPrefs.DeleteKey("DailyRecipe");
        PlayerPrefs.DeleteKey("DailyRecipeDate");

        // Save a recipe from an earlier day.
        PlayerPrefs.SetString("DailyRecipe", "Pancit");
        PlayerPrefs.SetString("DailyRecipeDate", "2026-10-13");

        List<string> unlockedRecipes = new List<string>
        {
            "Pancit",
            "Butter Chicken",
            "Katsudon"
        };

        // Use a fixed date so the test does not depend on the computer's clock.
        string requestedDate = "2030-01-02";

        // Act: request the daily recipe for the new date.
        string selectedRecipe = DailyRecipeSelector.GetDailyRecipeForDate(unlockedRecipes, requestedDate);

        // Assert: a valid recipe is returned and the requested date is saved.
        Assert.IsNotNull(selectedRecipe);
        Assert.Contains(selectedRecipe, unlockedRecipes);
        Assert.AreEqual(requestedDate, PlayerPrefs.GetString("DailyRecipeDate"));

        // Clean up saved values after the test.
        PlayerPrefs.DeleteKey("DailyRecipe");
        PlayerPrefs.DeleteKey("DailyRecipeDate");
    }
}
