using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelStepManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    // store and give values of level and its steps
    
    public Level currentLevel;

    private string sceneToLoad;
    private int currentStepIndex = 0;

    private bool waitForNext;

    private void Awake()
    {
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
                sceneToLoad = "Frying";
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
        
        step.isCompleted = true;
    }

    private IEnumerator WaitForStep(LevelStep step)
    {
        sceneLoader.LoadScene(sceneToLoad);

        while (!step.isCompleted)
        {
            Debug.Log("Waiting for step to complete...");
            yield return null;
        }
        yield return new WaitForSeconds(1f); // delay before next step - maybe add animation later

        Debug.Log($"Step {step.stepType} completed!");
        waitForNext = true;
        yield return null;
    }

    private void EndOfLevel()
    {
        Debug.Log("Level Complete!");
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
            if (currentStepIndex <= currentLevel.steps.Count)
            {
                StartStep(currentLevel.steps[currentStepIndex]); // to start next step
            }
            else
            {
                currentStepIndex--;
                EndOfLevel();
            }
        }

    }


    // timer to points
}
