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

    [SerializeField] private List<CookMeat> meatList;
    [SerializeField] private List<MonoBehaviour> meatsList;


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

        //if (AllMeatCooked())
        //{
        //    Debug.Log("All meat cooked!");
        //    // StartCoroutine(CompleteLevel());
        //    OnStepComplete?.Invoke();
        //}
    }


}
