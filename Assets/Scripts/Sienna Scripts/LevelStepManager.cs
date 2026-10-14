using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelStepManager : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private ProgressBarController progressBar;

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
        // Ensure that level is still marked complete when rebooting game.
        // Prefer PlayerPrefs unlocked level (persists across editor sessions) so we don't depend on
        // PlayerDataManager load order. Fall back to playerProfile if needed.
        var unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (unlockedLevel > currentLevel.levelNumber)
        {
            currentLevel.levelIsCompleted = true;
        }
        else if (PlayerDataManager.Instance != null && PlayerDataManager.Instance.playerProfile != null)
        {
            // If profile reports completed levels, mark completed when completedLevels >= levelNumber
            if (PlayerDataManager.Instance.playerProfile.getCompletedLevels() >= currentLevel.levelNumber)
            {
                currentLevel.levelIsCompleted = true;
            }
        }

        finalDish.SetActive(false);
        levelCompleteScreen.SetActive(false);
        currentStepIndex = 0;
        StartStep(currentLevel.steps[currentStepIndex]);

        progressBar.SetProgressValues(0f, currentLevel.steps.Count);
    }


    // scene based on enum
    private void StartStep(LevelStep step)
    {
        step.isCompleted = false;

        sceneToLoad = step.sceneOfStep;

        StartCoroutine(WaitForStep(step));
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

        progressBar.ProgressIncrease();
        sceneLoader.UnloadScene(sceneToLoad);


        yield return null;
    }

    private IEnumerator CompleteLevel()
    {
        currentLevel.levelIsCompleted = true;

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
        MixedIngredientsManager.OnStepComplete += HandleRecipeComplete; //Brooke added
    }

    private void OnDisable()
    {
        CutBowl.OnStepComplete -= HandleRecipeComplete;
        FryingStepManager.OnStepComplete -= HandleRecipeComplete;
        BoiledBowl.OnStepComplete -= HandleRecipeComplete;
        PlatingFoods.OnStepComplete -= HandleRecipeComplete;
        MixedIngredientsManager.OnStepComplete -= HandleRecipeComplete; //Brooke added
    }

    // complete level panel
    public void ReturnToLevelMap()
    {
        UpdateCompletedLevel();
        SceneManager.LoadScene("Level Map");
    }
    public void ReturnToMenu()
    {
        sceneLoader.LoadScene("MainMenu");
    }

    private async void UpdateCompletedLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Use the current level's explicit levelNumber to determine whether to unlock the next level.
        // This avoids relying on LevelMenu.currentLevel which may not be set reliably when the scene is loaded.
        var thisLevelNumber = (currentLevel != null) ? currentLevel.levelNumber : LevelMenu.currentLevel;

        if (thisLevelNumber == unlockedLevel)
        {
            unlockedLevel++;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();
            Debug.Log("Unlocked level: " + unlockedLevel);
        }

        if (!currentLevel.levelIsCompleted)
        {
            PlayerDataManager.Instance.playerProfile.addCompletedLevel();
            await PlayerDataManager.Instance.SavePlayerData();
            currentLevel.levelIsCompleted = true;
        }
    }
}
