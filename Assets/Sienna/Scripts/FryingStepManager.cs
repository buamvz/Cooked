using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FryingStepManager : MonoBehaviour, IStepCompleter
{
    public static Action OnStepComplete;

    [SerializeField] private List<CookMeat> meatList;
    [SerializeField] private List<MonoBehaviour> meatsList;


    private bool completeLevel;

    [SerializeField]  private SceneLoader sceneLoader;

    private void Start()
    {
        
        completeLevel = false;
    }

    void Update()
    {
        if (completeLevel)
            return;

        if (AllMeatCooked())
        {
            Debug.Log("All meat cooked!");
            // StartCoroutine(CompleteLevel());
            OnStepComplete?.Invoke();
        }
    }

    public void HideMeat()
    {
        for (int i = 0; i < meatList.Count; i++)
        {
            meatList[i].gameObject.SetActive(false);
        }
    }
    public void ShowMeat()
    {
        for (int i = 0; i < meatList.Count; i++)
        {
            meatList[i].gameObject.SetActive(true);
        }
    }

    public bool AllMeatCooked()
    {
        if (meatList.Count == 0) return false;

        for (int i = 0; i < meatList.Count; i++)
        {
            if (!meatList[i].isCooked) return false;
        }
        return true;
    }


}
