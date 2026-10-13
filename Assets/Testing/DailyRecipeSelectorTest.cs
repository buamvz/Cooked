
using System.Collections.Generic;
using NUnit.Framework;

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
        string selectedRecipe = DailyRecipeSelector.SelectRecipe(availableRecipes);

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
        string selectedRecipe = DailyRecipeSelector.SelectRecipe(unlockedRecipes);

        //The selected recipe must not be locked.
        Assert.Contains(selectedRecipe, unlockedRecipes);
    }
}
