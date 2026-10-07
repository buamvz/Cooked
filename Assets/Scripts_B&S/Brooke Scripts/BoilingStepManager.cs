using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BoilingStepManager : MonoBehaviour
{

    //dont even need this script anymore 
    [SerializeField] private List<BoilFood> noodlesList;
    [SerializeField] private GameObject levelCompleteScreen;

    private bool completeLevel;

    [SerializeField] private SceneLoader sceneLoader;

    private void Start()
    {
        levelCompleteScreen.SetActive(false);
        completeLevel = false;
    }

    private void Update()
    {
        if (completeLevel)
            return;

        if (AllNoodlesBoiled())
        {
            StartCoroutine(CompleteLevel());
        }
    }

    public bool AllNoodlesBoiled()
    {
        for (int i = 0; i < noodlesList.Count; i++)
        {
            if (!noodlesList[i].IsBoiled())
                return false;
        }

        return true;
    }

    public IEnumerator CompleteLevel()
    {
        Debug.Log("Boiling Level Complete!");

        yield return new WaitForSeconds(1f);

        completeLevel = true;

        levelCompleteScreen.SetActive(true);
    }

    public void GoToNextLevel()
    {
        Debug.Log("Go to next level...");

        sceneLoader.LoadScene("Level Map");
    }

    public void ReturnToMenu()
    {
        sceneLoader.LoadScene("MainMenu");
    }
}
