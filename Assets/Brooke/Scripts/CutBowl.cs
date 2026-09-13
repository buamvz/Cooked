using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RequiredFood
{
    public string foodID;
    public int requiredAmount;
    [HideInInspector] public int currentAmount;
}

public class CutBowl : MonoBehaviour
{
    [Header("Required Food")]
    [SerializeField] private List<RequiredFood> requiredFoods;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FoodID food = other.GetComponent<FoodID>();

        if (food == null)
            return;

        AddFood(food.foodID);
    }

    private void AddFood(string foodID)
    {
        foreach (RequiredFood food in requiredFoods)
        {
            if (food.foodID == foodID)
            {
                if (food.currentAmount < food.requiredAmount)
                {
                    food.currentAmount++;

                    Debug.Log(
                        foodID + ": " +
                        food.currentAmount + "/" +
                        food.requiredAmount
                    );

                    CheckRecipeComplete();
                }

                return;
            }
        }

        Debug.Log(foodID + " is not required for this recipe.");
    }

    private void CheckRecipeComplete()
    {
        foreach (RequiredFood food in requiredFoods)
        {
            if (food.currentAmount < food.requiredAmount)
            {
                return;
            }
        }

        Debug.Log("Recipe complete!");

        // We'll load the next scene here later.
    }
}
