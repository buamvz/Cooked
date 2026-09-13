using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CookMeat[] meatList;
    [SerializeField] private List<MonoBehaviour> meatsList;
    [SerializeField] private GameObject levelCompleteScreen;

    private bool completeLevel;

    [SerializeField]  private SceneLoader sceneLoader;

    private void Start()
    {
        levelCompleteScreen.SetActive(false);
        completeLevel = false;
    }

    private void Awake()
    {
        
    }

    void Update()
    {
        if (completeLevel)
            return;

        if (AllMeatCooked())
        {
            StartCoroutine(CompleteLevel());
        }
    }

    public bool AllMeatCooked()
    {
        for (int i = 0; i < meatList.Length; i++)
        {
            if (!meatList[i].isCooked) return false;
        }
        return true;
    }

    public IEnumerator CompleteLevel()
    {
        Debug.Log("Level Complete!");

        yield return new WaitForSeconds(1f);
        completeLevel = true;

        levelCompleteScreen.SetActive(true);
    }

    // complete level panel
    public void GoToNextLevel()
    {
        Debug.Log("Go to next level... Next level does not exist.");
        sceneLoader.LoadScene("Level Map");
    }
    public void ReturnToMenu()
    {
        sceneLoader.LoadScene("MainMenu");
    }
}
