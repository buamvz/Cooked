using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class RequiredFood
{
    public string foodID;
    public int requiredAmount;
    [HideInInspector] public int currentAmount;
}

public class CutBowl : MonoBehaviour, IStepCompleter
{
    public static event Action OnStepComplete;

    [Header("Required Food")]
    [SerializeField] private List<RequiredFood> requiredFoods;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("something in bowl");
        FoodID food = other.GetComponent<FoodID>();

        if (food == null)
            return;

        AddFood(food.foodID);
    }

    //how much of food added into bowl along with its id (so that same food can be added but also checks amount)
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

    //checks if all required foods have been added to the bowl
    private void CheckRecipeComplete()
    {
        foreach (RequiredFood food in requiredFoods)
        {
            if (food.currentAmount < food.requiredAmount)
            {
                return;
            }
        }

        OnStepComplete?.Invoke();
        Debug.Log("Recipe complete!");

    }
}
