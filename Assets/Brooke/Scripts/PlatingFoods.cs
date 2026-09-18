using System.Collections.Generic;
using UnityEngine;

public class PlatingFoods : MonoBehaviour
{
    [Header("Foods to Plate")]
    [SerializeField] private List<GameObject> requiredFoods;

    [Header("Plate Sprites")]
    [SerializeField] private Sprite finalPlateSprite;

    private SpriteRenderer spriteRenderer;

    private List<GameObject> foodsOnPlate = new List<GameObject>();

    private bool plateComplete = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (plateComplete)
            return;

        GameObject food = other.gameObject;

        // cheks if this is one of the foods required for the plate
        if (requiredFoods.Contains(food))
        {
            if (!foodsOnPlate.Contains(food))
            {
                foodsOnPlate.Add(food);

                Debug.Log(food.name + " placed on plate.");
            }
        }

        CheckPlate();
    }

    private void CheckPlate()
    {
        //check every required food
        foreach (GameObject food in requiredFoods)
        {
            if (!foodsOnPlate.Contains(food))
            {
                return;
            }
        }

        CompletePlate();
    }

    private void CompletePlate()
    {
        plateComplete = true;

        Debug.Log("All food has been plated!");

        //hides all the food objects
        foreach (GameObject food in requiredFoods)
        {
            if (food != null)
            {
                food.SetActive(false);
            }
        }

        //changes the plate to the the other sprite
        if (finalPlateSprite != null)
        {
            spriteRenderer.sprite = finalPlateSprite;
        }

        Debug.Log("Final plated meal created!");
    }
}