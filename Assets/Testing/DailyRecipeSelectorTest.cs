
using System.Collections.Generic;
using NUnit.Framework;

public class DailyRecipeSelectorTests
{
    [Test]
    public void SelectsRecipeFromAvailableRecipes()
    {
        // Arrange: create a list of recipes the player can access.
        List<string> availableRecipes = new List<string>
        {
            "Pancit",
            "Butter Chicken",
            "Katsudon"
        };

        // Act: placeholder for the daily recipe selection.
        // We will replace this with the real selector implementation.
        string selectedRecipe = DailyRecipeSelector.SelectRecipe(availableRecipes);

        // Assert: the selected recipe must be one of the available recipes.
        Assert.Contains(selectedRecipe, availableRecipes);
    }
}
