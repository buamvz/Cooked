using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CookingIngredients : MonoBehaviour, IStepCompleter
{
    public static Action OnStepComplete;

    [Header("Ingredients to Cook")]
    [SerializeField] private List<GameObject> requiredIngredients;

    [SerializeField] private GameObject finishedFood;

    //referecnce the dail flame script so that food will only cook/mix when dial is on
    [SerializeField] private FlameOn flameOnScript;

    private List<GameObject> foodsInPan = new List<GameObject>();
    [SerializeField] private Transform stovePosition;
    [SerializeField] private Collider2D panCollider;
    [SerializeField] private Collider2D stoveCollider;
    [Header("Pan Dragging")]
    [SerializeField] private DragObject panDragObjectScript;

    [SerializeField] private bool isPanOnFlame = false;
    public bool cookingComplete = false;
    private bool isCooking = false;

    public void Update()
    {
        //check if pan is on flame
        PanOnFlame();

        if (cookingComplete || isCooking)
            return;

        CheckPan();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cookingComplete)
            return;

        GameObject ingredient = other.gameObject;
        //checks if this is one of the ingredients required for the recipe
        if (requiredIngredients.Contains(ingredient))
        {
            if (!foodsInPan.Contains(ingredient))
            {
                foodsInPan.Add(ingredient);
                Debug.Log(ingredient.name + " was placed in pan.");
            }
        }
    }

    private void CheckPan()
    {
        if (!isPanOnFlame)
        {
            return;
        }
        //check every required ingredient
        foreach (GameObject ingredient in requiredIngredients)
        {
            if (!foodsInPan.Contains(ingredient))
            {
                return;
            }
        }
        //making sure flame is on from dial
        if (flameOnScript == null)
        {
            Debug.Log("flame on script missing");
            return;
        }

        if(!flameOnScript.isFlameOn)
        {
            return;
        }

        //everything is in pan and flame is on so start cooking
        StartCoroutine(CompleteCooking());
    }

    private IEnumerator CompleteCooking()
    {
        isCooking = true;

        Debug.Log("all ingredients are in the pan and the flame is on");

        //wait for 3 seconds to simulate cooking time
        yield return new WaitForSeconds(2f);

        cookingComplete = true;

        //hide all the ingredients
        foreach (GameObject ingredient in requiredIngredients)
        {
            if (ingredient != null)
            {
                ingredient.SetActive(false);
            }
        }

        //show the finished/mixed ingeredients
        if (finishedFood != null)
        {
            finishedFood.SetActive(true);
            Debug.Log("cooking complete");
        }
        else
        {
            Debug.Log("mixed ingredients object is missing");
        }

        yield return new WaitForSeconds(2f);

        OnStepComplete?.Invoke();
        Debug.Log("cooking step complete");
    }

    public void PanOnFlame()
    {
        if (panCollider == null || stoveCollider == null)
        {
            return;
        }

        if (panCollider.IsTouching(stoveCollider))
        {
            if (!isPanOnFlame)
            {
                isPanOnFlame = true;

                Debug.Log("Pan is on the stove.");

                //turn on all required ingredients so now they can be put onto pan
                foreach (GameObject ingredient in requiredIngredients)
                {
                    if (ingredient != null)
                    {
                        ingredient.SetActive(true);
                        Debug.Log(ingredient.name + " is now active.");
                    }
                }
            }

            //snaps pan to stove position - same as siennas
            if (stovePosition != null)
            {
                transform.position = stovePosition.position;
            }

            //stop the pan from being dragged
            if (panDragObjectScript != null)
            {
                panDragObjectScript.enabled = false;
            }
        }
        else
        {
            isPanOnFlame = false;

            //allow pan to be dragged again
            if (panDragObjectScript != null && !cookingComplete)
            {
                panDragObjectScript.enabled = true;
            }
        }
    }
}
