using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class MixedIngredientsManager : MonoBehaviour, IStepCompleter
{
    public static Action OnStepComplete;

    [SerializeField] private List<CookingIngredients> finishedFood;


    private bool completeLevel;

    [SerializeField] private SceneLoader sceneLoader;

    private void Start()
    {

        completeLevel = false;
    }

    void Update()
    {
        if (completeLevel)
            return;

        if (AllIngredientsMixed())
        {
            Debug.Log("All meat cooked!");
            // StartCoroutine(CompleteLevel());
            OnStepComplete?.Invoke();
        }
    }

    public bool AllIngredientsMixed()
    {
        if (finishedFood.Count == 0) return false;

        for (int i = 0; i < finishedFood.Count; i++)
        {
            if (!finishedFood[i].cookingComplete) return false;
        }
    }

}
