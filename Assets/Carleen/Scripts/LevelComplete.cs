using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void OnLevelComplete()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (LevelMenu.currentLevel == unlockedLevel)
        {
            unlockedLevel++;

            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();

            Debug.Log("Unlocked level: " + unlockedLevel);
        }

        SceneManager.LoadScene("Level Map");
    }
}