using System.Collections.Generic;
using UnityEngine;

public class BoiledBowl : MonoBehaviour
{
    [Header("Required Food")]
    [SerializeField] private List<RequiredBoiledFood> requiredFoods;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FoodID food = other.GetComponent<FoodID>();

        if (food == null)
            return;

        AddFood(food.foodID);
    }

    private void AddFood(string foodID)
    {
        foreach (RequiredBoiledFood food in requiredFoods)
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

        Debug.Log(foodID + " is not needed for this recipe."
        );
    }

    private void CheckRecipeComplete()
    {
        foreach (RequiredBoiledFood food in requiredFoods)
        {
            if (food.currentAmount < food.requiredAmount)
            {
                return;
            }
        }

        Debug.Log("Boiled recipe complete!");

        //remeber too add the scene changer later
    }
}

[System.Serializable]
public class RequiredBoiledFood
{
    public string foodID;
    public int requiredAmount;

    [HideInInspector]
    public int currentAmount;
}
