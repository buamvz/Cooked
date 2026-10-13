
using System.Collections.Generic;
using UnityEngine;

public class DailyRecipeSelector
{
    // Selects one recipe randomly from the recipes provided.
    public static string SelectRecipe(List<string> availableRecipes)
    {
        // Return null if there are no recipes to choose from.
        if (availableRecipes == null || availableRecipes.Count == 0)
        {
            return null;
        }

        // Generate a random index within the list's valid range.
        int randomIndex = Random.Range(0, availableRecipes.Count);

        // Return the recipe at that random position.
        return availableRecipes[randomIndex];
    }
}
