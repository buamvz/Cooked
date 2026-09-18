using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelStepManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    // store and give values of level and its steps
    
    public Level currentLevel;

    private string sceneToLoad;
    private int currentStepIndex = 0;

    private bool waitForNext;

    [Header("Level Complete Screen")]
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private GameObject finalDish;


    private void Awake()
    {
        finalDish.SetActive(false);
        levelCompleteScreen.SetActive(false);
        currentStepIndex = 0;
        StartStep(currentLevel.steps[currentStepIndex]);
    }


    // scene based on enum
    private void StartStep(LevelStep step)
    {
        step.isCompleted = false;

        switch (step.stepType)
        {
            case Step.Cutting:
                sceneToLoad = "Cutting";
                // set sceneToLoad to cutting
                break;
            case Step.Frying:
                sceneToLoad = "FryingPan";
                // frying
                break;
            case Step.Boiling:
                sceneToLoad = "Boiling";
                // boiling
                break;
            case Step.Plating:
                sceneToLoad = "Plating";
                // plating
                break;
        }

        StartCoroutine(WaitForStep(step));
        // load scene
        // check if complete condition met
    }

    private IEnumerator WaitForStep(LevelStep step)
    {
        sceneLoader.LoadSceneAdditive(sceneToLoad);

        while (!step.isCompleted)
        {
            // Debug.Log("Waiting for step to complete...");
            yield return null;
        }
        yield return new WaitForSeconds(1f); // delay before next step - maybe add animation later

        Debug.Log($"Step {step.stepType} completed!");
        waitForNext = true;

        sceneLoader.UnloadScene(sceneToLoad);

        yield return null;
    }

    private IEnumerator CompleteLevel()
    {
        Debug.Log("Level Complete!");
        levelCompleteScreen.SetActive(true);
        finalDish.SetActive(true);

        yield return new WaitForSeconds(1f);

        waitForNext = false;
    }

    // play next step when one is completed
    // if all are completed, level is completed
    private void Update()
    {

        if (waitForNext)
        {
            waitForNext = false;
            currentStepIndex++;
            if (currentStepIndex < currentLevel.steps.Count)
            {
                StartStep(currentLevel.steps[currentStepIndex]); // to start next step
            }
            else
            {
                StartCoroutine(CompleteLevel());
            }
        }

    }


    // === event reading for complete steps from other scripts === 
    private void HandleRecipeComplete()
    {
        if (currentLevel.steps[currentStepIndex] != null)
        {
            currentLevel.steps[currentStepIndex].isCompleted = true;
        }
    }

    private void OnEnable()
    {
        CutBowl.OnStepComplete += HandleRecipeComplete;
        FryingStepManager.OnStepComplete += HandleRecipeComplete;
        BoiledBowl.OnStepComplete += HandleRecipeComplete;
        PlatingFoods.OnStepComplete += HandleRecipeComplete;
    }

    private void OnDisable()
    {
        CutBowl.OnStepComplete -= HandleRecipeComplete;
        FryingStepManager.OnStepComplete -= HandleRecipeComplete;
        BoiledBowl.OnStepComplete -= HandleRecipeComplete;
        PlatingFoods.OnStepComplete -= HandleRecipeComplete;
    }

    // complete level panel
    public void ReturnToLevelMap()
    {
        Debug.Log("Go to next level... Next level does not exist.");
        sceneLoader.LoadScene("Level Map");
    }
    public void ReturnToMenu()
    {
        sceneLoader.LoadScene("MainMenu");
    }
}
